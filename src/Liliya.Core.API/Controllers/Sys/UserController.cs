using Liliya.Dto.Sys.User;
using Liliya.Services.Sys.User;
using Liliya.Shared;
using Liliya.Shared.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liliya.Core.API.Controllers.Sys
{
    /// <summary>
    /// 用户管理
    /// </summary>
    //[Authorize]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOptions<AuthrizeToken> _authrizeToken;

        public UserController(IUserService userService, IOptions<AuthrizeToken> authrizeToken)
        {
            _userService = userService;
            _authrizeToken = authrizeToken;
        }

        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult> InsertAsync([FromBody] UserInputDto input)
        {
            return await _userService.InsertAsync(input);
        }

        /// <summary>
        /// 修改一个用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult> UpdateAsync([FromBody] UserInputDto input)
        {
            return await _userService.UpdateAsync(input);
        }

        /// <summary>
        /// 根据Id删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult> DeleteAsync(Guid id)
        {
            return await _userService.DeleteAsync(id);
        }

        /// <summary>
        /// 根据Id加载用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult> GetByIdAsync(Guid id)
        {
            return await _userService.GetByIdAsync(id);
        }


        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult> GetAllAsync() 
        {
            Console.WriteLine(_authrizeToken.Value.Audience);
            return await _userService.GetAllAsync();
        }
    }
}
