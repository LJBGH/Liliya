using Liliya.Shared;
using Liliya.Shared.AppSetting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// SqlSugar仓储层拓展
/// </summary>
namespace Liliya.SqlSugar.Repository
{
    public class SqlSugarRepository<T> : ISqlSugarRepository<T> where T : class, new()
    {
        private ILogger _logger = null;
        public SqlSugarClient _dbContext;
        private IUserAuth _akliaUser;

        public SqlSugarRepository(IServiceProvider serviceProvider)
        {
            var conn = Appsettings.app(new string[] { "Liliya", "DbContexts", "ConnectionString" });
            var dbtype = Appsettings.app(new string[] { "Liliya", "DbContexts", "DataBaseType" });
            this._logger = serviceProvider.GetLogger(GetType());
            this._dbContext = SqlSugarDbFactory.GetSqlSugarDb(conn, dbtype, _logger);
            this._akliaUser = (serviceProvider.GetService(typeof(IUserAuth)) as IUserAuth);
        }

        /// <summary>
        /// 当前用户Id
        /// </summary>
        private Guid _userId => _akliaUser.Id;

        /// <summary>
        /// 获取DbContext
        /// </summary>
        /// <returns></returns>
        public SqlSugarClient DbContext()
        {
            return _dbContext;
        }

        /// <summary>
        /// 使用事务
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<AjaxResult> IUnitOfWork(Func<Task<AjaxResult>> func)
        {
            try
            {
                _dbContext.BeginTran();
                var result = await func.Invoke();
                if (!result.Success)
                {
                    _dbContext.RollbackTran();
                }
                _dbContext.CommitTran();
                return result;
            }
            catch (Exception e)
            {
                //事务回滚
                _dbContext.RollbackTran();
                return new AjaxResult(e.Message, AjaxResultType.Fail);
            }
        }

        #region Add添加 

        /// <summary>
        /// 增加单条数据
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns>操作是否成功</returns>
        public async Task<bool> AddAsync(T entity)
        {
            entity.NotNull(nameof(entity));
            entity = entity.CheckInsert<T>(_akliaUser);
            return await _dbContext.Insertable<T>(entity).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 增加多条数据
        /// </summary>
        /// <param name="list">实体集合</param>
        /// <returns>操作是否成功</returns>
        public async Task<bool> AddRangeAsync(List<T> entitys)
        {
            entitys.NotNull(nameof(entitys));
            entitys = entitys.CheckInsertRange<T>(_akliaUser);
            return await _dbContext.Insertable<T>(entitys).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 单条插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AjaxResult> InsertAsync(T entity)
        {
            entity.NotNull(nameof(entity));
            entity = entity.CheckInsert<T>(_akliaUser);
            var issuccess = await _dbContext.Insertable<T>(entity).ExecuteCommandAsync() > 0;
            return new AjaxResult(issuccess == true ? ResultMessage.InsertSuccess : ResultMessage.InsertFail, issuccess == true ? AjaxResultType.Success : AjaxResultType.Fail);
        }

        /// <summary>
        /// 无实体检查插入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<AjaxResult> InsertNoCheckAsync(T entity) 
        {
            entity.NotNull(nameof(entity));
            var issuccess = await _dbContext.Insertable<T>(entity).ExecuteCommandAsync() > 0;
            return new AjaxResult(issuccess == true ? ResultMessage.InsertSuccess : ResultMessage.InsertFail, issuccess == true ? AjaxResultType.Success : AjaxResultType.Fail);
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AjaxResult> InsertRangeAsync(List<T> entitys)
        {
            entitys.NotNull(nameof(entitys));
            entitys = entitys.CheckInsertRange<T>(_akliaUser);
            var issuccess = await _dbContext.Insertable<T>(entitys).ExecuteCommandAsync() > 0;
            return new AjaxResult(issuccess == true ? ResultMessage.InsertSuccess : ResultMessage.InsertFail, issuccess == true ? AjaxResultType.Success : AjaxResultType.Fail);
        }

        #endregion

        #region  Update更新

        /// <summary>
        /// 单条更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<AjaxResult> UpdateAsync(T entity)
        {
            entity.NotNull(nameof(entity));
            entity = entity.CheckUpdate<T>(_akliaUser);
            var issuccess = await _dbContext.Updateable<T>(entity).ExecuteCommandAsync() > 0;
            return new AjaxResult(issuccess == true ? ResultMessage.UpdateSuccess : ResultMessage.UpdateFail, issuccess == true ? AjaxResultType.Success : AjaxResultType.Fail);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public async Task<AjaxResult> UpdateRangeAsync(List<T> entitys)
        {
            entitys.NotNull(nameof(entitys));
            entitys = entitys.CheckUpdateRange<T>(_akliaUser);
            var issuccess = await _dbContext.Updateable<T>(entitys).ExecuteCommandAsync() > 0;
            return new AjaxResult(issuccess == true ? ResultMessage.UpdateSuccess : ResultMessage.UpdateFail, issuccess == true ? AjaxResultType.Success : AjaxResultType.Fail);
        }
        #endregion


        #region   Delete删除

        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<AjaxResult> DeleteAsync<Tkey>(Tkey id) 
        {
            id.NotNull(nameof(id));
            var entity = await this.GetByIdAsync(id);
            if (entity == null)
                return new AjaxResult("对象不存在，删除失败", AjaxResultType.Fail);

            var entity1 = entity.CheckDelete(out bool isSoft);
            if (isSoft)
            {
                var count = await _dbContext.Updateable(entity).ExecuteCommandAsync();
                return new AjaxResult(count > 0 ? ResultMessage.DeleteSuccess : ResultMessage.DeleteFail, count > 0 ? AjaxResultType.Success : AjaxResultType.Fail);
            }
            else 
            {
                var count = await _dbContext.Deleteable(entity).ExecuteCommandAsync();
                return new AjaxResult(count > 0 ? ResultMessage.DeleteSuccess : ResultMessage.DeleteFail, count > 0 ? AjaxResultType.Success : AjaxResultType.Fail);
            }
        }

        /// <summary>
        /// 单条删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<AjaxResult> DeleteAsync(T entity)
        {
            entity.NotNull(nameof(entity));

            var entity1 = entity.CheckDelete<T>(out bool isSoft);

            if (isSoft)
            {
                var count = await _dbContext.Updateable<T>(entity).ExecuteCommandAsync();
                return new AjaxResult(count > 0 ? ResultMessage.DeleteSuccess : ResultMessage.DeleteFail, count > 0 ? AjaxResultType.Success : AjaxResultType.Fail);
            }
            else
            {
                var count = await _dbContext.Deleteable<T>(entity).ExecuteCommandAsync();
                return new AjaxResult(count > 0 ? ResultMessage.DeleteSuccess : ResultMessage.DeleteFail, count > 0 ? AjaxResultType.Success : AjaxResultType.Fail);
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public async Task<AjaxResult> DeleteRangeAsync(List<T> entitys)
        {
            entitys.NotNull(nameof(entitys));
            entitys = entitys.CheckDeleteRange<T>(out bool isSoft);
            if (isSoft)
            {
                var count = await _dbContext.Updateable<T>(entitys).ExecuteCommandAsync();
                return new AjaxResult(count > 0 ? ResultMessage.DeleteSuccess : ResultMessage.DeleteFail, count > 0 ? AjaxResultType.Success : AjaxResultType.Fail);
            }
            else
            {
                var count = await _dbContext.Deleteable<T>(entitys).ExecuteCommandAsync();
                return new AjaxResult(count > 0 ? ResultMessage.DeleteSuccess : ResultMessage.DeleteFail, count > 0 ? AjaxResultType.Success : AjaxResultType.Fail);
            }
        }

        /// <summary>
        /// 拉姆达表达式删除
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<AjaxResult> DeleteByLambdaAsync(Expression<Func<T, bool>> expression)
        {
            expression.NotNull(nameof(expression));
            try
            {
                var entitys = await _dbContext.Queryable<T>().Where(expression).ToListAsync();
                if(entitys==null)
                    return new AjaxResult("删除失败，对象不存在",  AjaxResultType.Fail);
                entitys = entitys.CheckDeleteRange<T>(out bool isSoft);
                if (isSoft)
                {
                    var count = await _dbContext.Updateable<T>(entitys).ExecuteCommandAsync();      
                    return new AjaxResult(count > 0 ? ResultMessage.DeleteSuccess : ResultMessage.DeleteFail, count > 0 ? AjaxResultType.Success : AjaxResultType.Fail);
                }
                else
                {
                    var count = await _dbContext.Deleteable<T>(entitys).ExecuteCommandAsync();
                    return new AjaxResult(count > 0 ? ResultMessage.DeleteSuccess : ResultMessage.DeleteFail, count > 0 ? AjaxResultType.Success : AjaxResultType.Fail);
                }
            }
            catch (Exception e)
            {
                return new AjaxResult(ResultMessage.DeleteFail, e.Message, AjaxResultType.Fail);
            }
        }
        #endregion

        #region   Query查询
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Queryable<T>().ToListAsync();
        }

        /// <summary>
        /// 拉姆达表达式查询一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<T> GetSingleByLambdaAsync(Expression<Func<T, bool>> expression)
        {
            expression.NotNull(nameof(expression));
            return await _dbContext.Queryable<T>().Where(expression).SingleAsync();
        }

        /// <summary>
        /// 拉姆达表达式查询所有
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<List<T>> GetByLambdaAsync(Expression<Func<T, bool>> expression)
        {
            expression.NotNull(nameof(expression));
            return await _dbContext.Queryable<T>().Where(expression).ToListAsync();
        }


        /// <summary>
        /// 根据主键Id查询
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync<Tkey>(Tkey id)
        {
            id.NotNull(nameof(id));

            var entity = new T();
            var isDeleted = entity.CheckLogicDelete();
            return await _dbContext.Queryable<T>().Where(isDeleted).InSingleAsync(id);
        }

        /// <summary>
        /// 获取所有返回IQueryable类型
        /// </summary>
        /// <returns></returns>
        public ISugarQueryable<T> GetIQueryableAsync()
        {
            return _dbContext.Queryable<T>();
        }


        ///// <summary>
        ///// 分页查询拓展
        ///// </summary>
        ///// <returns></returns>
        //public async Task<IPageResult<T>> GetPageListAsync(PageRequest request)
        //{
        //    request.NotNull(nameof(request));
        //    Expression<Func<T, bool>> expression = null;
        //    expression = request == null ? null : FilterHelper.GetExpression<T>(request.queryFilter);

        //    RefAsync<int> total = 0;
        //    var pageInfo = await _dbContext.Queryable<T>().Where(expression).ToPageListAsync(request.PageIndex, request.PageRow, total); ;
        //    var result = new PageResult<T>(total, pageInfo);
        //    return result;
        //}


        ///// <summary>
        ///// 分页获取拓展
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //public async Task<IPageResult<T>> GetPageListAsync(LinqExpressionModel request)
        //{
        //    Expression<Func<T, bool>> expression = null;
        //    expression = request == null ? null : LinqExpressionParser.ParserConditions<T>(request.SelectConditionModel);

        //    RefAsync<int> total = 0;
        //    var pageInfo = await _dbContext.Queryable<T>().Where(expression).ToPageListAsync(request.PageIndex, request.PageRow, total); ;

        //    var result = new PageResult<T>(total, pageInfo);
        //    return result;
        //}


        #endregion




        #region  实体检查拓展
        /// <summary>
        /// 插入拓展
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private T InsertExpend(T entity)
        {
            System.Reflection.PropertyInfo[] properties = entity.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                switch (property.Name)
                {
                    //case "Id":
                    //    if (property.PropertyType == typeof(Guid))
                    //        property.SetValue(entity, Guid.NewGuid());
                    //    break;
                    case "CreatedAt": property.SetValue(entity, DateTime.Now); break;
                    case "CreatedId": property.SetValue(entity, _userId); break;
                    case "LastModifedAt": property.SetValue(entity, DateTime.Now); break;
                    case "LastModifyId": property.SetValue(entity, _userId); break;
                }
            }
            return entity;
        }

        /// <summary>
        /// 批量插入拓展
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        private List<T> InsertRangeExpend(List<T> entitys)
        {
            for (int i = 0; i < entitys.Count; i++)
            {
                entitys[i] = InsertExpend(entitys[i]);
            }
            return entitys;
        }

        /// <summary>
        /// 修改拓展
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private T UpdateExpend(T entity)
        {
            System.Reflection.PropertyInfo[] properties = entity.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                switch (property.Name)
                {
                    case "LastModifedAt": property.SetValue(entity, DateTime.Now); break;
                    case "LastModifyId": property.SetValue(entity, _userId); break;
                }
            }
            return entity;
        }

        /// <summary>
        /// 批量修改拓展
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        private List<T> UpdateRangeExpend(List<T> entitys)
        {
            for (int i = 0; i < entitys.Count; i++)
            {
                entitys[i] = UpdateExpend(entitys[i]);
            }
            return entitys;
        }

        /// <summary>
        /// 删除拓展
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private T DeleteExpend(T entity)
        {
            System.Reflection.PropertyInfo[] properties = entity.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                switch (property.Name)
                {
                    case "IsDeleted": property.SetValue(entity, true); break;
                }
            }
            return entity;
        }

        /// <summary>
        /// 批量删除拓展
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        private List<T> DeleteRangeExpend(List<T> entitys)
        {
            for (int i = 0; i < entitys.Count; i++)
            {
                entitys[i] = DeleteExpend(entitys[i]);
            }
            return entitys;
        }
        #endregion

    }
}
