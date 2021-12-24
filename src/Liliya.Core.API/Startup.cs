using Liliya.Core.API.Startups;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Liliya.AspNetCore.Filter;
using Liliya.WebSockets;
using Liliya.WebSockets.WatsonWebSocket;

namespace Liliya.Core.API
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
            services.AddControllers(x=> 
            {
                x.Filters.Add<AuditActionFilter>(); //审计日志过滤器
                x.Filters.Add<PermissionAuthorizationFilter>(); //授权过滤器
            });

            //公共拓展模块注入
            services.AddCommonService(Configuration);

            //WatsonWebSocket测试
            //WatsonWebSocketHelper.StartWatsonWebsocket();

        }

        //Autofac模块注入
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //服务层注入
            builder.ServicesInject();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //公共组件配置
            app.UseCommonExtension();


            app.UseRouting();

            //授权认证中间件必须配置路由之后，不然会报错
            app.UseAuthentication(); //认证
            app.UseAuthorization(); //授权

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
