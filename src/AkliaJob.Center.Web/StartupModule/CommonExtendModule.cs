using AkliaJob.Shared;
using Microsoft.Extensions.DependencyInjection;
using AkliaJob.Swagger;
using Microsoft.AspNetCore.Builder;
using AkliaJob.AutoMapper;


namespace AkliaJob.Center.Web.StartupModule
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

            service.AddHttpContextAccessor();
            service.AddSingleton<IAkliaUser, AkliaUser>();

            //AutoMapper注入
            service.AddAutoMapperService();
        }





        //公共中间件
        public static IApplicationBuilder UseCommonExtension(this IApplicationBuilder app) 
        {
            //Consul配置
            //app.UseConsul();

            //Swagger配置
            app.UseSwaggerService();

            return app;
        }

    }
}
