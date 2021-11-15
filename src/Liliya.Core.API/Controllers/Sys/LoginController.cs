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
    [Authorize(/*Policy = CostomGlobalPolicy.Name*/)]
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
        public async Task<AjaxResult> SignInAsync([FromBody] LoginInputDto input) 
        {
            return await _loginService.SignInAsync(input);
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
        public async Task<AjaxResult> UpdatePasswordAsync([FromBody] PasswordDto input) 
        {
            return await _loginService.UpdatePasswordAsync(input);
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult> RefreshAccessTokenAsync([FromBody] RefreshTokenDto input) 
        {
            return await _loginService.RefreshAccessTokenAsync(input);
        }
    }
}
