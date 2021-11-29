using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Shared
{
    /// <summary>
    /// 查询条件定义
    /// </summary>
    public class PageRequest
    {
        public PageRequest()
        {
            OrderConditions = new List<OrderCondition>();
            QueryFilter = new QueryFilter();
        }

        /// <summary>
        /// 当前页起始号，从1开的
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页的尺寸
        /// </summary>
        public int PageRow { get; set; }

        /// <summary>
        /// 查询条件模型
        /// </summary>
        public QueryFilter QueryFilter { get; set; }

        /// <summary>
        /// 排序条件集合
        /// </summary>
        public List<OrderCondition> OrderConditions { get; set; }
    }

    /// <summary>
    /// 查询条件模型
    /// </summary>
    public class QueryFilter
    {
        public QueryFilter()
        {
            QueryConditions = new List<QueryCondition>();
        }

        /// <summary>
        /// 查询条件类型
        /// </summary>
        public ConditionType ConditionType { get; set; }

        /// <summary>
        /// 查询条件集合
        /// </summary>
        public List<QueryCondition> QueryConditions { get; set; }
    }

    /// <summary>
    /// 查询条件模型
    /// </summary>
    public class QueryCondition
    {
        /// <summary>
        /// 查询字段名称
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 值类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 查询操作类型
        /// </summary>
        public LinqSelectOperator Operator { get; set; }
    }

    /// <summary>
    /// 排序模型
    /// </summary>
    public class OrderCondition
    {
        /// <summary>
        /// 查询字段名称
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 排序类型
        /// </summary>
        public LinqOrderType OrderType { get; set; }
    }

    /// <summary>
    /// 执行表达式类型
    /// </summary>
    public enum LinqSelectOperator
    {
        Contains = 0,        //包含
        Equal = 1,          //等于
        Greater = 2,        //大于
        GreaterEqual = 3,   //大于等于
        Less = 4,           //小于
        LessEqual = 5,      //小于等于
        NotEqual = 6,       //不等于
        InWithEqual = 7,    //对于多个值执行等于比较
        InWithContains = 8, //对于多个值执行包含比较
        Between = 9,
    }

    /// <summary>
    /// 排序类型
    /// </summary>
    public enum LinqOrderType
    {
        /// <summary>
        /// 正序
        /// </summary>
        ASC,
        /// <summary>
        /// 倒序
        /// </summary>
        DESC,
    }

    /// <summary>
    /// 查询条件类型
    /// </summary>
    public enum ConditionType
    {
        /// <summary>
        /// 并且
        /// </summary>
        And,
        /// <summary>
        /// 或者
        /// </summary>
        Or
    }
}
