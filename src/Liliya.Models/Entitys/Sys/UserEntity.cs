using Liliya.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Liliya.Models.Entitys
{

    /// <summary>
    /// 用户表
    /// </summary>
    [SugarTable("sys_User",TableDescription = "用户表")]
    public class UserEntity : IFullAuditedEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true/*, IsIdentity = true*/,ColumnDescription = "主键Id")]
        [DisplayName("主键Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [DisplayName("账号")]
        [SugarColumn(IsNullable = true,Length = 50, ColumnDescription = "账号")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        [SugarColumn(IsNullable = true, Length = 50, ColumnDescription = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        [DisplayName("人员姓名")]
        [SugarColumn(IsNullable = true,Length = 50, ColumnDescription = "人员姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [DisplayName("工号")]
        [SugarColumn(IsNullable = true,Length = 50, ColumnDescription = "工号")]
        public string JobNumber { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [DisplayName("部门")]
        [SugarColumn(IsNullable = true,Length = 50, ColumnDescription = "部门")]
        public string Department { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        [DisplayName("职位")]
        [SugarColumn(IsNullable = true,Length = 50, ColumnDescription = "职位")]
        public string Position { get; set; }






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
