using AutoMapper;
using Liliya.Common.Excel;
using Liliya.Models.Entitys.Sys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Liliya.Dto.Sys.User
{
    /// <summary>
    /// 用户信息导出
    /// </summary>
    [AutoMap(typeof(UserEntity))]
    public class UserExportDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        [DisplayName("账号")]
        [ExcelColumnName("账号", ColumnWith = 15, Sort = 1)]
        public string Account { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        [DisplayName("姓名")]
        [ExcelColumnName("姓名", ColumnWith = 20, Sort = 2)]
        public string Name { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [DisplayName("工号")]
        [ExcelColumnName("工号", ColumnWith = 10, Sort = 3)]
        public string JobNumber { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [DisplayName("部门")]
        [ExcelColumnName("部门", ColumnWith = 20, Sort = 4)]
        public string Department { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        [DisplayName("职位")]
        [ExcelColumnName("职位", ColumnWith = 20, Sort = 5,HyperLink = "https://www.baidu.com/")]
        public string Position { get; set; }
    }
}
