using Liliya.Core.API.Event;
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
    [Authorize(/*Policy = CostomGlobalPolicy.Name*/)]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;

        private readonly IEventBus _eventBus;

        public UserController(IUserService userService,IEventBus eventBus)
        {
            _userService = userService;
            _eventBus = eventBus;
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


        /// <summary>
        /// 测试接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<AjaxResult> TestAsync() 
        {
            await _eventBus.PublishAsync(new TestEvent("测试事件"));
            return new AjaxResult("测试成功", AjaxResultType.Success);
        }
    }
}
