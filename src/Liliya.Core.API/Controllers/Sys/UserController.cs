using Liliya.Dto.Sys.User;
using Liliya.Services.Sys.User;
using Liliya.Shared;
using Liliya.Shared.Controller;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liliya.Core.API.Controllers.Sys
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
            return await _userService.GetAllAsync();
        }
    }
}
