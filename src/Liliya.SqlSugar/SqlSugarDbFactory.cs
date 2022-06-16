using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.SqlSugar
{
    /// <summary>
    /// DB工厂，获取sqlsugarDb
    /// </summary>
    public class SqlSugarDbFactory
    {
        public static SqlSugarClient GetSqlSugarDb(string conn, string dbType, ILogger logger)
        {
            if ("MySql".Equals(dbType, StringComparison.CurrentCultureIgnoreCase))
            {
                return GetDb(conn, DbType.MySql, logger);
            }
            if ("SqlServer".Equals(dbType, StringComparison.CurrentCultureIgnoreCase))
            {
                return GetDb(conn, DbType.SqlServer, logger);
            }

            return GetDb(conn, DbType.MySql, logger);
        }

        public static SqlSugarClient GetDb(string conn, DbType dbType, ILogger logger)
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = conn,//连接符字串
                DbType = dbType,// DbType.SqlServer,
                IsAutoCloseConnection = true,  //自动关闭连接
                InitKeyType = InitKeyType.Attribute,//从特性读取主键自增信息
            });

            //sql执行前事件
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                //logger.LogInformation($"{ DateTime.Now.ToString()}  SQL执行前: \r\n{sql}");
            };
            //sql执行完成事件
            db.Aop.OnLogExecuted = (sql, pars) =>
            {
                logger.LogInformation($"{ DateTime.Now.ToString()}  SQL执行成功: \r\n{sql}");
            };
            //sql报错事件
            db.Aop.OnError = (exp) =>//SQL报错
            {
                logger.LogError($"{ DateTime.Now.ToString()}  SQL报错: \r\n{exp.Message}");
            };
            return db;
        }
    }
}
