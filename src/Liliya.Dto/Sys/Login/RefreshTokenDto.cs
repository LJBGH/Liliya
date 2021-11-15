using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Dto.Sys.Login
{
    public class RefreshTokenDto : LoginInputDto
    {
        /// <summary>
        /// 登录后授权的 Token
        /// </summary>
        public string Token { get; set; }
    }
}
