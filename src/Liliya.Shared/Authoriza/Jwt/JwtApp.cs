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
        private readonly IOptions<AuthrizeToken> _authrizeToken;

        public JwtApp(IHttpContextAccessor accessor, IDistributedCache cache, IOptions<AuthrizeToken> authrizeToken)
        {
            _accessor = accessor;
            _cache = cache;
            _authrizeToken = authrizeToken;
        }




        /// <summary>
        /// 用户名
        /// </summary>
        public string Name => _accessor.HttpContext.User.Identity.Name;

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
        public string GenerateToken(List<Claim> claims)
        {
            var now = DateTime.Now;

            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authrizeToken.Value.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken
              (
              issuer: _authrizeToken.Value.Issuer,//设置发行人
              audience: _authrizeToken.Value.Audience,//设置订阅人
              claims: claims,//设置角色
              notBefore: now,//开始时间
              expires: now.AddMinutes(_authrizeToken.Value.ExpireMins),//设置过期时间
              signingCredentials: creds
              );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }

        /// <summary>
        /// 停用Token(将Token加入Redis黑名单中)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> DeactivateTokenAsync() 
        {
            try
            {
                await _cache.SetStringAsync(
                    GetToken(),
                    " ",
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_authrizeToken.Value.ExpireMins)
                    });
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
