using Liliya.Dto.Sys.DataDictionary;
using Liliya.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Liliya.Services.Sys.DataDictionary
{
    public interface IDataDictionaryService
    {
        /// <summary>
        /// 添加数据字典
        /// </summary>
        /// <returns></returns>
        Task<AjaxResult> InsertAsync(DataDictionaryInputDto input);

        /// <summary>
        /// 修改数据字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AjaxResult> UpdateAsync(DataDictionaryInputDto input);

        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AjaxResult> DeleteAsync(Guid id);

        /// <summary>
        /// 获取数据字典树形
        /// </summary>
        /// <returns></returns>
        Task<AjaxResult> GetTreeAsync();
    }
}
