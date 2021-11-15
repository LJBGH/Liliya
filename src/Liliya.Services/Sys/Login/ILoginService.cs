using Liliya.Dto.Sys.Login;
using Liliya.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Liliya.Services.Sys.Login
{
    public interface ILoginService
    {

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        Task<AjaxResult> SignInAsync(LoginInputDto input);


        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        Task<AjaxResult> SignOutAsync();


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        Task<AjaxResult> UpdatePasswordAsync(PasswordDto input);
    }
}
