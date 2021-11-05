using AkliaJob.Shared;
using AkliaJob.Shared.AppSetting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AkliaJob.SqlSugar.Repository
{
    public class SqlSugarRepository<T> : ISqlSugarRepository<T> where T : class, new()
    {
        public SqlSugarClient _dbContext;
        private IConfiguration _configuration;
        private IAkliaUser _akliaUser;
        private ILogger _logger = null;

        public SqlSugarRepository(IServiceProvider serviceProvider)
        {
            //_configuration = configuration;
            //var conn = configuration["AkliaJob:MySql:DbContexts:ConnectionString"];
            //var dbtype = configuration["AkliaJob:MySql:DbContexts:DataBaseType"];
            var conn = Appsettings.app(new string[] { "AkliaJob", "DbContexts", "ConnectionString" });
            var dbtype = Appsettings.app(new string[] { "AkliaJob", "DbContexts", "DataBaseType" });
            this._logger = serviceProvider.GetLogger(GetType());
            this._dbContext = SqlSugarDbFactory.GetSqlSugarDb(conn, dbtype, _logger);
            _akliaUser = (serviceProvider.GetService(typeof(IAkliaUser)) as IAkliaUser);
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
                return new AjaxResult(e.Message, AjaxResultType.Error);
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
            entity = entity.CheckInsert<T>(_akliaUser);
            var issuccess = await _dbContext.Insertable<T>(entity).ExecuteCommandAsync() > 0;
            return new AjaxResult(issuccess == true ? ResultMessage.InsertSuccess : ResultMessage.InsertFail, issuccess == true ? AjaxResultType.Success : AjaxResultType.Error);
        }

        /// <summary>
        /// 无实体检查插入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<AjaxResult> InsertNoCheckAsync(T entity) 
        {
            var issuccess = await _dbContext.Insertable<T>(entity).ExecuteCommandAsync() > 0;
            return new AjaxResult(issuccess == true ? ResultMessage.InsertSuccess : ResultMessage.InsertFail, issuccess == true ? AjaxResultType.Success : AjaxResultType.Error);
        }



        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<AjaxResult> InsertRangeAsync(List<T> entitys)
        {
            entitys = entitys.CheckInsertRange<T>(_akliaUser);
            var issuccess = await _dbContext.Insertable<T>(entitys).ExecuteCommandAsync() > 0;
            return new AjaxResult(issuccess == true ? ResultMessage.InsertSuccess : ResultMessage.InsertFail, issuccess == true ? AjaxResultType.Success : AjaxResultType.Error);
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
            entity = entity.CheckUpdate<T>(_akliaUser);
            var issuccess = await _dbContext.Updateable<T>(entity).ExecuteCommandAsync() > 0;
            return new AjaxResult(issuccess == true ? ResultMessage.UpdateSuccess : ResultMessage.UpdateFail, issuccess == true ? AjaxResultType.Success : AjaxResultType.Error);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public async Task<AjaxResult> UpdateRangeAsync(List<T> entitys)
        {
            entitys = entitys.CheckUpdateRange<T>(_akliaUser);
            var issuccess = await _dbContext.Updateable<T>(entitys).ExecuteCommandAsync() > 0;
            return new AjaxResult(issuccess == true ? ResultMessage.UpdateSuccess : ResultMessage.UpdateFail, issuccess == true ? AjaxResultType.Success : AjaxResultType.Error);
        }

        #endregion

        #region   Delete删除
        /// <summary>
        /// 单条删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<AjaxResult> DeleteAsync(T entity)
        {
            var entity1 = entity.CheckDelete<T>(out bool isSoft);

            if (isSoft)
            {
                var count = await _dbContext.Updateable<T>(entity).ExecuteCommandAsync();
                return new AjaxResult(count > 0 ? ResultMessage.DeleteSuccess : ResultMessage.DeleteFail, count > 0 ? AjaxResultType.Success : AjaxResultType.Error);
            }
            else
            {
                var count = await _dbContext.Deleteable<T>(entity).ExecuteCommandAsync();
                return new AjaxResult(count > 0 ? ResultMessage.DeleteSuccess : ResultMessage.DeleteFail, count > 0 ? AjaxResultType.Success : AjaxResultType.Error);
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public async Task<AjaxResult> DeleteRangeAsync(List<T> entitys)
        {
            entitys = entitys.CheckDeleteRange<T>(out bool isSoft);
            if (isSoft)
            {
                var count = await _dbContext.Updateable<T>(entitys).ExecuteCommandAsync();
                return new AjaxResult(count > 0 ? ResultMessage.DeleteSuccess : ResultMessage.DeleteFail, count > 0 ? AjaxResultType.Success : AjaxResultType.Error);
            }
            else
            {
                var count = await _dbContext.Deleteable<T>(entitys).ExecuteCommandAsync();
                return new AjaxResult(count > 0 ? ResultMessage.DeleteSuccess : ResultMessage.DeleteFail, count > 0 ? AjaxResultType.Success : AjaxResultType.Error);
            }
        }

        /// <summary>
        /// 拉姆达表达式删除
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<AjaxResult> DeleteByLambdaAsync(Expression<Func<T, bool>> expression)
        {
            try
            {
                var entitys = await _dbContext.Queryable<T>().Where(expression).ToListAsync();
                entitys = entitys.CheckDeleteRange<T>(out bool isSoft);
                if (isSoft)
                {
                    var count = await _dbContext.Updateable<T>(entitys).ExecuteCommandAsync();
                    return new AjaxResult(count > 0 ? ResultMessage.DeleteSuccess : ResultMessage.DeleteFail, count > 0 ? AjaxResultType.Success : AjaxResultType.Error);
                }
                else
                {
                    var count = await _dbContext.Deleteable<T>(entitys).ExecuteCommandAsync();
                    return new AjaxResult(count > 0 ? ResultMessage.DeleteSuccess : ResultMessage.DeleteFail, count > 0 ? AjaxResultType.Success : AjaxResultType.Error);
                }
            }
            catch (Exception e)
            {
                return new AjaxResult(ResultMessage.DeleteFail, e.Message, AjaxResultType.Error);
            }
        }
        #endregion

        #region   Query查询
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetAsync()
        {

            return await _dbContext.Queryable<T>().ToListAsync();
        }



        /// <summary>
        /// 拉姆达表达式查询一条
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbContext.Queryable<T>().Where(expression).SingleAsync();
        }

        /// <summary>
        /// 拉姆达表达式查询所有
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
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
            return await _dbContext.Queryable<T>().InSingleAsync(id);
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




    }
}
