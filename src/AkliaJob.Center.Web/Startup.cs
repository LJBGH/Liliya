using AkliaJob.Center.Web.StartupModule;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AkliaJob.Shared;

namespace AkliaJob.Center.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            //公共拓展模块注入
            services.AddCommonService();
        }

        //Autofac模块注入
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //服务层和仓储层注入
            builder.ServicesAndRepositoryInject();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //公共组件配置
            app.UseCommonExtension();

            //自定义异常中间件
            app.CustomerMiddleware();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
