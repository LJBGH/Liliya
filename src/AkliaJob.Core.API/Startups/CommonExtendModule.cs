using AkliaJob.Shared;
using Microsoft.Extensions.DependencyInjection;
using AkliaJob.Swagger;
using Microsoft.AspNetCore.Builder;
using AkliaJob.AutoMapper;
using AkliaJob.SqlSugar.Repository;

namespace AkliaJob.Core.API.Startups
{

    public static class CommonExtendModule
    {
        /// <summary>
        /// 公共拓展模块注入
        /// </summary>
        public static void AddCommonService(this IServiceCollection service) 
        {
            //swagger注入
            service.AddSwaggerService();

            //AutoMapper注入
            service.AddAutoMapperService();

            //SqlSugar泛型仓储注入
            service.AddScoped(typeof(ISqlSugarRepository<>), typeof(SqlSugarRepository<>));

            //Http上下文和用户信息注入
            service.AddHttpContextAccessor();
            service.AddSingleton<IAkliaUser, AkliaUser>();
        }





        //公共中间件
        public static IApplicationBuilder UseCommonExtension(this IApplicationBuilder app) 
        {
            //Consul服务注册发现配置
            //app.UseConsul();

            //Swagger配置
            app.UseSwaggerService();

            return app;
        }

    }
}
