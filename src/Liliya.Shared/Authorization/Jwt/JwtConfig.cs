using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Shared
{
    /// <summary>
    /// Jwt认证配置信息
    /// </summary>
    public class JwtConfig
    {
        /// <summary>
        /// 需要加密的key
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// token是谁颁发的  发布者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// token可以给哪些客户端使用  订阅者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Token过期时间
        /// </summary>
        public double ExpireMins { get; set; }
    }
}
