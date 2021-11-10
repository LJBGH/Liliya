using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Liliya.Shared
{
    /// <summary>
    /// 实体检查
    /// </summary>
    public static class CheckEntity
    {
        /// <summary>
        /// 插入检查
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="akliaUser"></param>
        /// <returns></returns>
        public static T CheckInsert<T>(this T entity, IAkliaUser akliaUser) where T : class
        {
            //判断T是否继承ICreatedAudited接口
            if (typeof(ICreatedAudited).IsAssignableFrom(typeof(T)))
            {
                ICreatedAudited createdAudited = (ICreatedAudited)entity;
                createdAudited.CreatedId = akliaUser.Id;
                createdAudited.CreatedAt = DateTime.Now;
                var entity1 = (T)createdAudited;
                return entity1;
            }
            else
            {
                return entity;
            }
        }

        /// <summary>
        /// 批量插入检查
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <param name="akliaUser"></param>
        /// <returns></returns>
        public static List<T> CheckInsertRange<T>(this List<T> entitys, IAkliaUser akliaUser) where T : class
        {
            //判断T是否继承ICreatedAudited接口
            if (typeof(ICreatedAudited).IsAssignableFrom(typeof(T)) && entitys.IsNotNull())
            {
                var list = new List<T>();
                foreach (var item in entitys)
                {
                    ICreatedAudited createdAudited = (ICreatedAudited)item;
                    createdAudited.CreatedId = akliaUser.Id;
                    createdAudited.CreatedAt = DateTime.Now;
                    var entity1 = (T)createdAudited;
                    list.Add(entity1);
                }
                return list;
            }
            else
            {
                return entitys;
            }
        }


        /// <summary>
        /// 更新检查
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="akliaUser"></param>
        /// <returns></returns>
        public static T CheckUpdate<T>(this T entity, IAkliaUser akliaUser)
        {
            //判断T是否继承IModifiedAudited接口
            if (typeof(IModifiedAudited).IsAssignableFrom(typeof(T)))
            {
                IModifiedAudited modifiedAudited = (IModifiedAudited)entity;
                modifiedAudited.LastModifyId = akliaUser.Id;
                modifiedAudited.LastModifedAt = DateTime.Now;
                var entity1 = (T)modifiedAudited;
                return entity1;
            }
            else
            {
                return entity;
            }
        }

        /// <summary>
        /// 批量更新检查
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <param name="akliaUser"></param>
        /// <returns></returns>
        public static List<T> CheckUpdateRange<T>(this List<T> entitys, IAkliaUser akliaUser)
        {
            //判断T是否继承IModifiedAudited接口
            if (typeof(IModifiedAudited).IsAssignableFrom(typeof(T)) && entitys.IsNotNull())
            {
                var list = new List<T>();
                foreach (var item in entitys)
                {
                    IModifiedAudited modifiedAudited = (IModifiedAudited)item;
                    modifiedAudited.LastModifyId = akliaUser.Id;
                    modifiedAudited.LastModifedAt = DateTime.Now;
                    var entity1 = (T)modifiedAudited;
                    list.Add(entity1);
                }

                return list;
            }
            else
            {
                return entitys;
            }
        }

        /// <summary>
        /// 删除检查
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static T CheckDelete<T>(this T entity, out bool isSoft)
        {
            //判断T是否继承ISpftDelete接口
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                ISoftDelete softDelete = (ISoftDelete)entity;
                softDelete.IsDeleted = true;
                var entity1 = (T)softDelete;
                isSoft = true;
                return entity;
            }
            else
            {
                isSoft = false;
                return entity;
            }
        }

        /// <summary>
        /// 批量删除检查
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static List<T> CheckDeleteRange<T>(this List<T> entitys, out bool isSoft)
        {
            List<T> result = new List<T>();
            //判断T是否继承ISpftDelete接口
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                foreach (var item in entitys)
                {
                    ISoftDelete softDelete = (ISoftDelete)item;
                    softDelete.IsDeleted = true;
                    var entity1 = (T)softDelete;
                    result.Add(entity1);
                }
                isSoft = true;
                return result;
            }
            else
            {
                isSoft = false;
                return entitys;
            }
        }


        /// <summary>
        /// 查询时,逻辑删除检查
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Expression<Func<T, bool>> CheckLogicDelete<T>(this T TEntity)                
        {
            //判断T是否继承ISpftDelete接口
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                //定义表达式变量
                ParameterExpression param = Expression.Parameter(typeof(T), "m");
                Expression key = param;

                //定义条件值
                object convertValue = false;
                Expression value = Expression.Constant(convertValue);

                //定义条件字段名
                key = Expression.Property(key, "IsDeleted");

                //生成表达式，并转为linq Where参数类型
                var expression =  Expression.Equal(key, Expression.Convert(value, key.Type)) as Expression;
                return Expression.Lambda<Func<T, bool>>(expression, param);
            }
            else 
            {
                return null;
            }
        }
    }
}
