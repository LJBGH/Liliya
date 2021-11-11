using Liliya.Shared;
using Microsoft.Extensions.DependencyInjection;
using Liliya.Swagger;
using Microsoft.AspNetCore.Builder;
using Liliya.AutoMapper;
using Liliya.SqlSugar.Repository;

namespace Liliya.Core.API.Startups
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
            //授权认证注入
            service.AddAuthService();
            //AutoMapper注入
            service.AddAutoMapperService();
            //SqlSugar泛型仓储注入
            service.AddScoped(typeof(ISqlSugarRepository<>), typeof(SqlSugarRepository<>));
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
