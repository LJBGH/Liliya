using Liliya.Models.Entitys.Sys;
using Liliya.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Liliya.Dto.Sys.DataDictionary
{
    [LiliyaAutoMapper(typeof(DataDictionaryEntity))]
    public class DataDictionaryInputDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public Guid Id { get; set; }


        /// <summary>
        /// 数据字典标题
        /// </summary>
        [DisplayName("数据字典标题")]
        public string Title { get; set; }

        /// <summary>
        /// 数据字典值
        /// </summary>
        [DisplayName("数据字典值")]
        public string Value { get; set; }

        /// <summary>
        /// 数据字典备注
        /// </summary>
        [DisplayName("数据字典备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 数据字典父级
        /// </summary>
        [DisplayName("数据字典父级")]
        public Guid ParentId { get; set; } = Guid.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }

        /// <summary>
        ///获取或设置 编码
        /// </summary>
        [DisplayName("唯一编码")]
        public string Code { get; set; }


    }
}
