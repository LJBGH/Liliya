﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Common.Excel
{
    /// <summary>
    /// 导入属性
    /// </summary>
    public class ExcelReadColumnNameAttribute : System.Attribute
    {
        public int Index
        {
            get;
        }
        public string ColumnName
        {
            get;
        }
        public ExcelReadColumnNameAttribute(string columnName)
        {
            ColumnName = columnName;
            Index = -1;
        }
        public ExcelReadColumnNameAttribute(int index)
        {
            Index = index;
        }
    }
}
