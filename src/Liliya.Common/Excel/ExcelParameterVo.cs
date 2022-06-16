using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Liliya.Common.Excel
{
    /// <summary>
    /// Excel列头帮助类
    /// </summary>
    public class ExcelParameterVo
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName
        {
            get;
            set;
        }
        /// <summary>
        /// 列宽
        /// </summary>
        public int ColumnWidth
        {
            get;
            set;
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort
        {
            get;
            set;
        }
        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo Property
        {
            get;
            set;
        }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool ShowState { get; set; }
    }
}
