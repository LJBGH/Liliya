using Liliya.Shared;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Liliya.SqlSugar.Repository
{
    public interface ISqlSugarRepository<T> where T : class, new()
    {
        /// <summary>
        /// 获取DbContext
        /// </summary>
        /// <returns></returns>
        SqlSugarClient DbContext();

        /// <summary>
        /// 工作单元
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<AjaxResult> IUnitOfWork(Func<Task<AjaxResult>> func);

        #region Add
        /// <summary>
        /// 增加单条数据返回Bool
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>操作是否成功</returns>
        Task<bool> AddAsync(T entity);

        /// <summary>
        /// 增加多条数据
        /// </summary>
        /// <param name="entitys">实体集合</param>
        /// <returns>操作是否成功</returns>
        Task<bool> AddRangeAsync(List<T> entitys);

        /// <summary>
        /// 单条插入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<AjaxResult> InsertAsync(T entity);

        /// <summary>
        /// 无实体检查插入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<AjaxResult> InsertNoCheckAsync(T entity);

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        Task<AjaxResult> InsertRangeAsync(List<T> entitys);

        #endregion

        #region   Update更新

        /// <summary>
        /// 单条更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<AjaxResult> UpdateAsync(T entity);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        Task<AjaxResult> UpdateRangeAsync(List<T> entitys);

        #endregion

        #region   Delete删除

        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<AjaxResult> DeleteAsync(T entity);

        /// <summary>
        /// 批量根据实体删除
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        Task<AjaxResult> DeleteRangeAsync(List<T> entitys);

        /// <summary>
        /// 拉姆达表达式删除
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<AjaxResult> DeleteByLambdaAsync(Expression<Func<T, bool>> expression);
        #endregion

        #region   Query查询

        /// <summary>
        /// 获取所有数据返回List<T>
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAsync();

        /// <summary>
        /// 拉姆达表达式查询返回T
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 拉姆达表达式查询一条,并返回T
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 根据主键Id查询,返回T
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync<Tkey>(Tkey id);

        /// <summary>
        /// 获取所有数据返回IQueryable<T>类型
        /// </summary>
        /// <returns></returns>
        ISugarQueryable<T> GetIQueryableAsync();


        ///// <summary>
        ///// 分页获取拓展
        ///// </summary>
        ///// <returns></returns>
        //Task<IPageResult<T>> GetPageListAsync(PageRequest request);

        ///// <summary>
        ///// 分页拓展
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //Task<IPageResult<T>> GetPageListAsync(LinqExpressionModel request);

        #endregion
    }
}
