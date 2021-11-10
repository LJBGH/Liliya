using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AkliaJob.Core.API.Startups
{
    public static class AutofacModule
    {

        public static void ServicesAndRepositoryInject(this ContainerBuilder builder) 
        {

            //var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath; //获取项目路径
            //var repositoryDllFile = Path.Combine(basePath, "AkliaJob.Repository.dll");
            //var serviceDllFile = Path.Combine(basePath, "AkliaJob.Services.dll");
            //if (!(File.Exists(repositoryDllFile)))
            //{
            //    throw new Exception("Repository.dll，因为项目解耦了，所以需要先F6编译，再F5运行，请检查 bin 文件夹，并拷贝。");
            //}
            //var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            //var assemblysService = Assembly.LoadFrom(serviceDllFile);


            //var serviceAssembly = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(x => x.FullName.Contains("AkliaJob.Services")).FirstOrDefault();
            //var reposiyoryAssembly = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(x => x.FullName.Contains("AkliaJob.Services")).FirstOrDefault();

            var assemblysService = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("AkliaJob.Services")).FirstOrDefault();
            //var assemblysRepository = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("AkliaJob.Repository")).FirstOrDefault();


            if (assemblysService == null) 
            {
                throw new Exception("服务层程序集未找到，或程序集已丢失，请重新编译运行");
            }

            //业务层注入
            builder.RegisterAssemblyTypes(assemblysService)
                //注入已实现的接口
                .AsImplementedInterfaces()
                //设置 生命周期 为Scope模式
                .InstancePerLifetimeScope();

            ////仓储层注入
            //builder.RegisterAssemblyTypes(assemblysRepository)
            //    .AsImplementedInterfaces()
            //    .InstancePerLifetimeScope();
        }
    }
}
