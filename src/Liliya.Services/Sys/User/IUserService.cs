using Liliya.Dto.Sys.User;
using Liliya.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Liliya.Services.Sys.User
{
    public interface IUserService
    {
        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AjaxResult> InsertAsync(UserInputDto input);

        /// <summary>
        /// 修改一个用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AjaxResult> UpdateAsync(UserInputDto input);

        /// <summary>
        /// 根据Id删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AjaxResult> DeleteAsync(Guid id);

        /// <summary>
        /// 根据Id加载用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AjaxResult> GetByIdAsync(Guid id);

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        Task<AjaxResult> GetAllAsync();

        /// <summary>
        /// 用户信息导入
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AjaxResult> ImportUserAsync(List<UserImportDto> input);

        /// <summary>
        /// 用户信息导出
        /// </summary>
        /// <returns></returns>
        Task<List<UserExportDto>> ExportUserAsync();


        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        Task<AjaxResult> TestAsync();

    }
}
