using Liliya.Core.API.Startups;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Liliya.Shared;

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
            services.AddControllers();

            //������չģ��ע��
            services.AddCommonService(Configuration);
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
