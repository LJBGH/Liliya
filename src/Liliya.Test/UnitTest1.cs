using Liliya.Models.Entitys.Sys;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlSugar;

namespace Liliya.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                //ConnectionString = "Data Source=101.34.154.180,51433;Initial Catalog=LabelTest;uid=sa;pwd=Pass@Word;",//连接符字串
                //DbType =  DbType.SqlServer,// DbType.SqlServer,
                //IsAutoCloseConnection = true,
                //InitKeyType = InitKeyType.Attribute,//从特性读取主键自增信息

                ConnectionString = "server=rm-uf61ev7xoo84r6707fo.mysql.rds.aliyuncs.com;userid=ljb_ali123;pwd=Llbali@Password; database=liliya;connectiontimeout=3000;port=3306;Pooling=true;Max Pool Size=300; Maximum Pool Size=1000;sslMode=None;",//连接符字串
                DbType = DbType.MySql,// DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键自增信息
            });

            //db.CodeFirst.InitTables(typeof(UserEntity));
            db.CodeFirst.InitTables(typeof(DataDictionaryEntity));

        }
    }
}
