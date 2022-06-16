using Liliya.AspNetCore.ApiBase;
using Liliya.Common.Excel;
using Liliya.Core.API.Event;
using Liliya.Dto.Sys.User;
using Liliya.Services.Sys.User;
using Liliya.Shared;
using Liliya.Shared.Attributes.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Liliya.Core.API.Controllers.Sys
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Authorize(/*Policy = CostomGlobalPolicy.Name*/)]
    [AuditedLog]
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
        /// 用户信息表解析
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<UserImportDto>> ParseUserExcelAsync(IFormFile file)
        {
            await Task.CompletedTask;
            var list = ExcelHelper<UserImportDto>.UpLoad(file, 0);
            return list;
        }

        /// <summary>
        /// 用户信息导入
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult> ImportUserAsync([FromBody] List<UserImportDto> input) 
        {
            return await _userService.ImportUserAsync(input);
        }

        /// <summary>
        /// 用户信息导出
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<FileResult> ExportUserAsync([FromServices] IWebHostEnvironment environment)
        {
            var userlist = await _userService.ExportUserAsync();

            string folderpath = Path.Combine(environment.WebRootPath, $"export");

            var fileName = $"用户信息-{DateTime.Now.ToString("yyyyMMddhhmmss")}-{RandomExtensions.GetRandom()}.xlsx";

            var file = ExcelHelper<UserExportDto>.SaveExcel(userlist, $"{folderpath}\\{fileName}");

            var filestream = new FileStream(file, FileMode.Open);

            return File(filestream, "application/vnd.ms-excel", fileName);
        }


        /// <summary>
        /// 用户信息导出测试
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<FileResult> ExportUserTest([FromServices] IWebHostEnvironment environment) 
        {
            await Task.CompletedTask;
            var userList = new List<UserExportDto>()
            {
                new UserExportDto
                {
                     Account  = "LiShen",
                     Name = "李仏",
                     JobNumber = "001",
                     Department = "研发部",
                     Position = "架构师"     
                }
            };

            string folderpath = Path.Combine(environment.WebRootPath, $"export");

            var fileName = $"用户信息-{DateTime.Now.ToString("yyyyMMddhhmmss")}-{RandomExtensions.GetRandom()}.xlsx";

            var file = ExcelHelper<UserExportDto>.SaveExcel(userList, $"{folderpath}\\{fileName}");

            var filestream = new FileStream(file, FileMode.Open);

            return File(filestream, "application/vnd.ms-excel", fileName);
        }


        /// <summary>
        /// 测试接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<AjaxResult> TestAsync() 
        {
            return await _userService.TestAsync();
            //await _eventBus.PublishAsync(new TestEvent("测试事件"));
            //return new AjaxResult("测试成功", AjaxResultType.Success);
        }
    }
}
