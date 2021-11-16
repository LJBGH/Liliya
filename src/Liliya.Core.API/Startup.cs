using Liliya.Core.API.Startups;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Liliya.Asp.NetCore.Filter;
using Liliya.Shared;
using Liliya.Core.API.Event;

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
                x.Filters.Add<AuditLogFilter>();
                x.Filters.Add<PermissionAuthorizationFilter>();
            });

            //������չģ��ע��
            services.AddCommonService(Configuration);

            //�¼�����ע��
            services.AddTransient<IEventHandler, TestEventHander>();
            services.AddSingleton<IEventBus, EventBus>();

        }

        //Autofacģ��ע��
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //�����ע��
            builder.ServicesInject();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //�����������
            app.UseCommonExtension();

            //�Զ����쳣�м��
            app.CustomerMiddleware();

            app.UseRouting();

            //��Ȩ��֤�м����������·��֮�󣬲�Ȼ�ᱨ��
            app.UseAuthentication(); //��֤
            app.UseAuthorization(); //��Ȩ

            //�����¼�����
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
