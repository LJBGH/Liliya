using Liliya.Dto.Sys.Login;
using Liliya.Services.Sys.Login;
using Liliya.Shared;
using Liliya.Shared.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liliya.Core.API.Controllers.Sys
{
    /// <summary>
    /// 登录模块
    /// </summary>
    [Authorize(Policy = CostomGlobalPolicy.Name)]
    public class LoginController : ApiControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<AjaxResult> LoginAsync(LoginInputDto input) 
        {
            return await _loginService.LoginAsync(input);
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult> SignOutAsync() 
        {
            return await _loginService.SignOutAsync();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult> UpdatePasswordAsync(PasswordDto input) 
        {
            return await _loginService.UpdatePasswordAsync(input);
        }
    }
}
