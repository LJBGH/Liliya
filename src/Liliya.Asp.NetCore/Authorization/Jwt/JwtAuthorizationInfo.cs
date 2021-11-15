using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Asp.NetCore.Authorization
{
    public class JwtAuthorizationInfo
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 授权时间
        /// </summary>
        public long Auths { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public long Expires { get; set; }

        /// <summary>
        /// 是否授权成功
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }
}
