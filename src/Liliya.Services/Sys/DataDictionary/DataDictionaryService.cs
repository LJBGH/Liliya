using Liliya.Dto.Sys.DataDictionary;
using Liliya.Models.Entitys.Sys;
using Liliya.Shared;
using Liliya.SqlSugar.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liliya.Services.Sys.DataDictionary
{
    public class DataDictionaryService : IDataDictionaryService
    {
        private readonly ISqlSugarRepository<DataDictionaryEntity> _dataDictionaryRepository;

        public DataDictionaryService(ISqlSugarRepository<DataDictionaryEntity> dataDictionaryRepository)
        {
            _dataDictionaryRepository = dataDictionaryRepository;
        }

        /// <summary>
        /// 添加数据字典
        /// </summary>
        /// <returns></returns>
        public async Task<AjaxResult> InsertAsync(DataDictionaryInputDto input)
        {
            input.NotNull(nameof(input));
            var data = input.MapTo<DataDictionaryEntity>();
            return await _dataDictionaryRepository.InsertAsync(data);
        }

        /// <summary>
        /// 修改数据字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AjaxResult> UpdateAsync(DataDictionaryInputDto input)
        {
            input.NotNull(nameof(input));
            var data = input.MapTo<DataDictionaryEntity>();
            return await _dataDictionaryRepository.UpdateAsync(data);
        }


        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<AjaxResult> DeleteAsync(Guid id)
        {
            id.NotNull(nameof(id));
            return await _dataDictionaryRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 获取数据字典树形
        /// </summary>
        /// <returns></returns>
        public async Task<AjaxResult> GetTreeAsync()
        {
            var list = await _dataDictionaryRepository.GetByLambdaAsync(x => x.IsDeleted == false);
            var dictList = list.MapToList<DataDictionaryTreeDto>().ToList();
            var rootList = dictList.Where(x => x.ParentId.IsNullOrEmpty()).ToList();
            DictionaryToTree(rootList, dictList);
            return new AjaxResult(ResultMessage.LoadSucces, rootList, AjaxResultType.Success);
        }


        /// <summary>
        /// 递归将字典列表转为树形
        /// </summary>
        /// <param name="rootList"></param>
        /// <param name="dictionaryList"></param>
        public void DictionaryToTree(List<DataDictionaryTreeDto> rootList, List<DataDictionaryTreeDto> dictList) 
        {
            if (rootList == null || rootList.Count == 0)
                return;
            foreach (var item in rootList)
            {
                var childrens = dictList.Where(x => x.ParentId == item.Id).ToList();
                if (childrens == null || childrens.Count == 0)
                    continue;
                if (childrens.Count > 0)
                    item.IsLeaf = true;
                item.Children.AddRange(childrens);
                item.Children.OrderBy(x => x.Sort);
                DictionaryToTree(childrens, dictList);
            }
        }
    }
}
