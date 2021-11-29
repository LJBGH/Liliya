using Liliya.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Liliya.Shared
{
    public class JwtApp : IJwtApp
    {
        /// <summary>
        /// HTTP请求上下文
        /// </summary>
        private readonly IHttpContextAccessor _accessor;

        /// <summary>
        /// 分布式缓存
        /// </summary>
        private readonly IDistributedCache _cache;

        /// <summary>
        /// Jwt配置类
        /// </summary>
        private readonly IOptions<JwtConfig> _jwtConfig;

        /// <summary>
        /// 已授权的 Token 信息集合      为什么使用ISet因为Token是不会有重复的元素
        /// </summary>
        private static ISet<JwtAuthorizationInfo> _tokens = new HashSet<JwtAuthorizationInfo>();



        public JwtApp(IHttpContextAccessor accessor, IDistributedCache cache, IOptions<JwtConfig> jwtConfig)
        {
            _accessor = accessor;
            _cache = cache;
            _jwtConfig = jwtConfig;
        }




        /// <summary>
        /// 用户名
        /// </summary>
        //public string Name => _accessor.HttpContext.User.Identity.Name;
        public string Name => GetClaimValueByType("UserName").FirstOrDefault();

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid Id => GetClaimValueByType("jti").FirstOrDefault().ToGuid();

        /// <summary>
        /// 得到Claims
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        /// <summary>
        /// 根据Claim类型获取值
        /// </summary>
        public List<string> GetClaimValueByType(string ClaimType)
        {
            return (from item in GetClaimsIdentity()
                    where item.Type == ClaimType
                    select item.Value).ToList();
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            return _accessor.HttpContext.Request.Headers["Authorization"].ObjToString().Replace("Bearer ", "");
        }

        /// <summary>
        /// 解析Token
        /// </summary>
        /// <param name="ClaimType"></param>
        /// <returns></returns>
        public List<string> GetUserInfoFromToken(string ClaimType)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            if (!string.IsNullOrEmpty(GetToken()))
            {
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(GetToken());
                return (from item in jwtToken.Claims
                        where item.Type == ClaimType
                        select item.Value).ToList();
            }
            else
            {
                return new List<string>() { };
            }
        }


        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="authrizeToken"></param>
        /// <returns></returns>
        public JwtAuthorizationInfo GenerateToken(JwtUser user)
        {
            user.NotNull(nameof(user));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,user.Id.ToString()),   //唯一标识,用户Id
                new Claim("Account",user.Account.ToString()),
                new Claim("UserName",user.Name),
            };

            var authTime = DateTime.Now;  //授权时间
            var expiresAt = authTime.AddMinutes(_jwtConfig.Value.ExpireMins); //过期时间

            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Value.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken
              (
              issuer: _jwtConfig.Value.Issuer,//设置发行人
              audience: _jwtConfig.Value.Audience,//设置订阅人
              claims: claims,//设置角色
              notBefore: authTime,//授权时间
              expires: expiresAt,//设置过期时间
              signingCredentials: creds
              );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            JwtAuthorizationInfo JwtAuthorization = new JwtAuthorizationInfo
            {
                UserId = user.Id,
                Token = token,
                Auths = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                Expires = new DateTimeOffset(expiresAt).ToUnixTimeSeconds(),
                Success = true
            };
            _tokens.Add(JwtAuthorization);
            return JwtAuthorization;
        }

        /// <summary>
        /// 停用Token(将Token加入Redis黑名单中)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task DeactivateTokenAsync() 
        {
            await _cache.SetStringAsync(
                   GetToken(),
                   " ",
                   new DistributedCacheEntryOptions
                   {
                       AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_jwtConfig.Value.ExpireMins)
                   });
        }


        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<JwtAuthorizationInfo> RefreshTokenAsync(JwtUser user, string token)
        {
            var jwtAuth = GetExistenceToken(token);
            if (jwtAuth == null) 
            {
                return new JwtAuthorizationInfo
                {
                    Token = "未获取到当前Token的信息",
                    Success = false
                };
            }

            var jwt = GenerateToken(user);

            //停用修改前的 Token 信息
            await DeactivateTokenAsync();

            return jwt;
        }

        /// <summary>
        /// 判断 Token 是否有效
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public async Task<bool> IsActiveAsync(string token) => await _cache.GetStringAsync(token) == null;

        /// <summary>
        /// 判断当前Token是否有效
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsCurrentActiveTokenAsync() => await IsActiveAsync(GetToken());

        /// <summary>
        /// 判断Token是否存在
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public JwtAuthorizationInfo GetExistenceToken(string token) => _tokens.SingleOrDefault(x => x.Token == token);
    }
}
