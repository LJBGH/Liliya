using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Dto.Sys.Login
{
    public class PasswordDto : LoginInputDto
    {
        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }
    }
}
