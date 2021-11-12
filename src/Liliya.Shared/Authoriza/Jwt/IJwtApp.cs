using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Liliya.Shared
{
    public interface IJwtApp
    {
        /// <summary>
        /// 用户名
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 用户ID
        /// </summary>
        Guid Id { get; }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IEnumerable<Claim> GetClaimsIdentity();
        /// <summary>
        /// 根据Claim类型获取值
        /// </summary>
        /// <param name="ClaimType"></param>
        /// <returns></returns>
        List<string> GetClaimValueByType(string ClaimType);
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        string GetToken();
        /// <summary>
        ///
        /// </summary>
        /// <param name="ClaimType"></param>
        /// <returns></returns>
        List<string> GetUserInfoFromToken(string ClaimType);


        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="authrizeToken"></param>
        /// <returns></returns>
        string GenerateToken(List<Claim> claims);

        /// <summary>
        /// 停用Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> DeactivateTokenAsync();
    }
}
