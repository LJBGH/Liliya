using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liliya.Test.Magicodes
{
    [ExcelExporter(Name="测试",AutoCenter =true,TableStyle = OfficeOpenXml.Table.TableStyles.Dark10)]
    public class UserExportDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [DisplayName("主键Id")]
        [ExporterHeader(DisplayName ="主键ID",IsAutoFit = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [DisplayName("账号")]
        [ExporterHeader(DisplayName = "账号", IsAutoFit = true)]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        [ExporterHeader(DisplayName = "密码", IsAutoFit = true)]
        public string Password { get; set; }

        /// <summary>
        /// 人员姓名
        /// </summary>
        [DisplayName("人员姓名")]
        [ExporterHeader(DisplayName = "人员姓名", IsAutoFit = true)]
        public string Name { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [DisplayName("工号")]
        [ExporterHeader(DisplayName = "工号", IsAutoFit = true)]
        public string JobNumber { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [DisplayName("部门")]
        [ExporterHeader(DisplayName = "部门", IsAutoFit = true)]
        public string Department { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        [DisplayName("职位")]
        [ExporterHeader(DisplayName = "职位", IsAutoFit = true)]
        public string Position { get; set; }



    }
}
