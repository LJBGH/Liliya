using Liliya.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Liliya.Models.Entitys.Sys
{
    /// <summary>
    /// 用户表
    /// </summary>
    [SugarTable("sys_DataDictionary", TableDescription = "数据字典表")]
    public class DataDictionaryEntity : IFullAuditedEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [DisplayName("主键Id")]
        [SugarColumn(IsPrimaryKey = true, ColumnDescription = "主键Id")]
        public Guid Id { get; set; }


        /// <summary>
        /// 数据字典标题
        /// </summary>
        [DisplayName("数据字典标题")]
        [SugarColumn(IsNullable = true,Length =50, ColumnDescription = "数据字典标题")]
        public string Title { get; set; }

        /// <summary>
        /// 数据字典值
        /// </summary>
        [DisplayName("数据字典值")]
        [SugarColumn(IsNullable = true,Length = 255, ColumnDescription = "数据字典值")]
        public string Value { get; set; }

        /// <summary>
        /// 数据字典备注
        /// </summary>
        [DisplayName("数据字典备注")]
        [SugarColumn(IsNullable = true, ColumnDataType = "longtext", ColumnDescription = "数据字典备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 数据字典父级
        /// </summary>
        [DisplayName("数据字典父级")]
        [SugarColumn( ColumnDescription = "数据字典父级")]
        public Guid ParentId { get; set; } = Guid.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        [SugarColumn(IsNullable = true, ColumnDescription = "排序")]
        public int Sort { get; set; }

        /// <summary>
        ///获取或设置 编码
        /// </summary>
        [DisplayName("唯一编码")]
        [SugarColumn(IsNullable = true,Length =255, ColumnDescription = "唯一编码")]
        public string Code { get; set; }






        #region   通用字段
        /// <summary>
        /// 创建人Id
        /// </summary>
        [DisplayName("创建人Id")]
        [SugarColumn(ColumnDescription = "创建人Id")]
        public Guid CreatedId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        [SugarColumn(IsNullable = true, ColumnDescription = "创建时间")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 最后修改人Id
        /// </summary>
        [DisplayName("最后修改人Id")]
        [SugarColumn(IsNullable = true, ColumnDescription = "最后修改人Id")]
        public Guid? LastModifyId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DisplayName("最后修改时间")]
        [SugarColumn(IsNullable = true, ColumnDescription = "最后修改时间")]
        public DateTime LastModifedAt { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        [SugarColumn(ColumnDescription = "是否删除")]
        public bool IsDeleted { get; set; }

        #endregion
    }
}
