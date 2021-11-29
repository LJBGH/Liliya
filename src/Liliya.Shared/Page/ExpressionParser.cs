using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Liliya.Shared
{
    /// <summary>
    /// 查询表达式拼接
    /// </summary>
    public static class ExpressionParser
    {
        public static Expression<Func<T, bool>> ParserConditions<T>(QueryFilter  queryFilter)
        {
            queryFilter.NotNull("queryFilter");
            //将条件转化成表达式的Body
            var parameter = Expression.Parameter(typeof(T));
            var query = ParseExpressionBody(queryFilter, parameter);
            return Expression.Lambda<Func<T, bool>>(query, parameter);
        }
        private static Expression ParseExpressionBody(QueryFilter  queryFilter, ParameterExpression parameter)
        {
            List<Expression> expressions = new List<Expression>();
            if (queryFilter == null || queryFilter.QueryConditions.Count() == 0)
            {
                return Expression.Constant(true, typeof(bool));
            }
            else if (queryFilter.QueryConditions.Count() == 1)
            {
                return ParseCondition(queryFilter.QueryConditions.First(), parameter);
            }
            else
            {
                foreach (var item in queryFilter.QueryConditions)
                {
                    expressions.Add(ParseCondition(item, parameter)); ;
                }
                return queryFilter.ConditionType == ConditionType.And
                    ? expressions.Aggregate(Expression.AndAlso)
                    : expressions.Aggregate(Expression.OrElse);
            }
        }
        //对查询条件进行处理
        private static Expression ParseCondition(QueryCondition  queryCondition, ParameterExpression parameter)
        {
            ParameterExpression p = parameter;
            Expression key;
            //对值进行转换处理
            object convertValue = queryCondition.Value;
            if (queryCondition.Type != null
                && queryCondition.Operator != LinqSelectOperator.InWithContains
                && queryCondition.Operator != LinqSelectOperator.InWithEqual)
            {
                if (queryCondition.Type.ToUpper() == "DATETIME")
                {
                    convertValue = System.Convert.ToDateTime(queryCondition.Value);
                }
                else if (queryCondition.Type.ToUpper() == "INT")
                {
                    convertValue = System.Convert.ToInt32(queryCondition.Value);
                }
                else if (queryCondition.Type.ToUpper() == "LONG")
                {
                    convertValue = System.Convert.ToInt64(queryCondition.Value);
                }
                else if (queryCondition.Type.ToUpper() == "DOUBLE")
                {
                    convertValue = System.Convert.ToDouble(queryCondition.Value);
                }
                else if (queryCondition.Type.ToUpper() == "BOOL")
                {
                    convertValue = System.Convert.ToBoolean(queryCondition.Value);
                }
            }
            Expression value = Expression.Constant(convertValue);
            ////参数化字段
            //if (queryCondition.ParentFields != null && queryCondition.ParentFields.Count > 0)
            //{
            //    key = p;
            //    foreach (var parent in queryCondition.ParentFields)
            //    {
            //        key = Expression.Property(key, parent);
            //    }
            //    key = Expression.Property(key, queryCondition.Field);
            //}
            //else
            //{
            //    key = Expression.Property(p, queryCondition.Field);
            //}
            key = Expression.Property(p, queryCondition.Field);
            switch (queryCondition.Operator)
            {
                case LinqSelectOperator.Contains:
                    return Expression.Call(key, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), value);
                case LinqSelectOperator.Equal:
                    return Expression.Equal(key, Expression.Convert(value, key.Type));
                case LinqSelectOperator.Greater:
                    return Expression.GreaterThan(key, Expression.Convert(value, key.Type));
                case LinqSelectOperator.GreaterEqual:
                    return Expression.GreaterThanOrEqual(key, Expression.Convert(value, key.Type));
                case LinqSelectOperator.Less:
                    return Expression.LessThan(key, Expression.Convert(value, key.Type));
                case LinqSelectOperator.LessEqual:
                    return Expression.LessThanOrEqual(key, Expression.Convert(value, key.Type));
                case LinqSelectOperator.NotEqual:
                    return Expression.NotEqual(key, Expression.Convert(value, key.Type));
                case LinqSelectOperator.InWithEqual:
                    return ParaseIn(key, queryCondition, true);
                case LinqSelectOperator.InWithContains:
                    return ParaseIn(key, queryCondition, false);
                case LinqSelectOperator.Between:
                    return ParaseBetween(key, queryCondition);
                default:
                    throw new NotImplementedException("不支持此操作");
            }
        }

        //对查询“Between"的处理
        private static Expression ParaseBetween(Expression key, QueryCondition  queryCondition)
        {
            //ParameterExpression p = parameter;
            //Expression key = Expression.Property(p, conditions.Field);
            var valueArr = queryCondition.Value.Split(',');
            if (valueArr.Length != 2)
            {
                throw new NotImplementedException("ParaseBetween参数错误");
            }
            try
            {
                int.Parse(valueArr[0]);
                int.Parse(valueArr[1]);
            }
            catch
            {
                throw new NotImplementedException("ParaseBetween参数只能为数字");
            }
            Expression expression = Expression.Constant(true, typeof(bool));
            //开始位置
            Expression startvalue = Expression.Constant(int.Parse(valueArr[0]));
            Expression start = Expression.GreaterThanOrEqual(key, Expression.Convert(startvalue, key.Type));

            Expression endvalue = Expression.Constant(int.Parse(valueArr[1]));
            Expression end = Expression.GreaterThanOrEqual(key, Expression.Convert(endvalue, key.Type));
            return Expression.AndAlso(start, end);
        }
        //对查询“in"的处理
        private static Expression ParaseIn(Expression key, QueryCondition  queryCondition, bool isEqual)
        {
            var valueArr = queryCondition.Value.Split(',');
            Expression expression = Expression.Constant(false, typeof(bool));
            foreach (var itemVal in valueArr)
            {
                object conValue = itemVal;
                Type keyType = key.Type;
                if (queryCondition.Type?.ToUpper() == "INT")
                {
                    conValue = System.Convert.ToInt32(itemVal);
                    keyType = typeof(int);
                }
                else if (queryCondition.Type?.ToUpper() == "LONG")
                {
                    conValue = System.Convert.ToInt64(itemVal);
                    keyType = typeof(long);
                }
                Expression value = Expression.Constant(conValue);
                Expression right;
                if (isEqual)
                    right = Expression.Equal(key, Expression.Convert(value, keyType));
                else
                    right = Expression.Call(key, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), value);
                expression = Expression.Or(expression, right);
            }
            return expression;
        }
    }
}
