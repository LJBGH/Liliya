using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AkliaJob.SqlSugar.Entity
{
    public class SqlSugarEntityBase<TKey>
    {

        [Description("主键")]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = istrue)]
        public TKey Id { get; set; }

        public const bool istrue = true;

        public static bool IsIntType(Type type) 
        {
            return type == typeof(int);
        }
    }
}
