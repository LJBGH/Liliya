using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Common.Excel
{
    /// <summary>
    /// 导出属性
    /// </summary>
    public class ExcelColumnNameAttribute : System.Attribute
    {
        private int _columnWith = 20;
        private bool _showState = true;
        private string _hyperlink = null;

        public ExcelColumnNameAttribute(string columnName)
        {
            ColumnName = columnName;
        }

        public string ColumnName
        {
            get;
        }

        /// <summary>
        /// 列宽
        /// </summary>
        public int ColumnWith
        {
            get
            {
                return this._columnWith;
            }
            set
            {
                this._columnWith = value;
            }
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
        /// 状态
        /// </summary>
        public bool ShowState
        {
            get
            {
                return this._showState;
            }
            set
            {
                this._showState = value;
            }
        }
        /// <summary>
        /// 超链接
        /// </summary>
        public string HyperLink 
        {
            get 
            {
                return _hyperlink;
            }
            set 
            {
                this._hyperlink = value;
            }
        }
    }
}
