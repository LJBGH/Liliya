using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Common.Excel
{
    public class ExcelColumnNameAttribute : System.Attribute
    {
        private int _columnWith = 20;
        private bool _showState = true;
        public string ColumnName
        {
            get;
        }

        //列宽
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
        public int Sort
        {
            get;
            set;
        }

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

        public ExcelColumnNameAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}
