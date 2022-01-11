using EFCore.Sharding;
using Liliya.Models.Entitys.Sys;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liliya.Test.EFSharding
{
    [TestClass]
    public class ShardingTest
    {
        [TestMethod]
        public void EFShardingTest() 
        {
            ServiceCollection services = new ServiceCollection();
            //配置初始化
            services.AddEFCoreSharding(config =>
            {
                var coon = "server=rm-uf61ev7xoo84r6707fo.mysql.rds.aliyuncs.com;userid=ljb_ali123;pwd=Llbali@Password; database=efshardingtest;connectiontimeout=3000;port=3306;Pooling=true;Max Pool Size=300; Maximum Pool Size=1000;sslMode=None;";

                config.UseDatabase<LiliyaDbAccessor>(coon, DatabaseType.MySql);


                ////添加数据源
                //config.AddDataSource(coon, ReadWriteType.Read | ReadWriteType.Write, DatabaseType.MySql);
                ////对3取模分表
                //config.SetHashModSharding<UserEntity>(nameof(UserEntity.Id), 3);
            });

            var serviceProvider = services.BuildServiceProvider();

            using var scop = serviceProvider.CreateScope();

            var db = scop.ServiceProvider.GetService<LiliyaDbAccessor>();
            var db1 = scop.ServiceProvider.GetService<IShardingDbAccessor>();

            db.InsertAsync<UserEntity>(new UserEntity { });
            db.GetIQueryable<UserEntity>();
            db1.GetIShardingQueryable<UserEntity>().Where(x => x.Id != Guid.Empty).ToList();

        }
    }
}
