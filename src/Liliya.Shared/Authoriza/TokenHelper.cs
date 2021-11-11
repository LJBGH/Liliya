using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Liliya.Shared
{
    public class TokenHelper
    {
        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="tokenManagement"></param>
        /// <returns></returns>
        public static string GenerateToken(List<Claim> claims, AuthrizeToken authrizeToken)
        {
            var now = DateTime.Now;

            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authrizeToken.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken
              (
              issuer: authrizeToken.Issuer,//设置发行人
              audience: authrizeToken.Audience,//设置订阅人
              claims: claims,//设置角色
              notBefore: now,//开始时间
              expires: now.AddMinutes(authrizeToken.ExpireMins),//设置过期时间
              signingCredentials: creds
              );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }
    }
}
