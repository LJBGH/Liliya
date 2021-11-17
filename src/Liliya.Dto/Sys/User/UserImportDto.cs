using Liliya.Common.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Liliya.Dto.Sys.User
{
    /// <summary>
    /// 用户信息导入Dto
    /// </summary>
    public class UserImportDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        [DisplayName("账号")]
        [ExcelReadColumnName("账号")]
        public string Account { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        [DisplayName("姓名")]
        [ExcelReadColumnName("姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [DisplayName("工号")]
        [ExcelReadColumnName("工号")]
        public string JobNumber { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [DisplayName("部门")]
        [ExcelReadColumnName("部门")]
        public string Department { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        [DisplayName("职位")]
        [ExcelReadColumnName("职位")]
        public string Position { get; set; }
    }
}
