using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Liliya.Serilog;
using System.IO;
using System;

namespace Liliya.Core.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory()) //���autofac����,�����������滻Ϊautofac����
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    //��SSL֤��
                    //.ConfigureKestrel(options =>
                    //{
                    //    options.Listen(System.Net.IPAddress.Any, 443, listenOptions =>
                    //      {
                    //          listenOptions.UseHttps(Path.Combine(AppContext.BaseDirectory, "liliya.work.pfx"), "134116");
                    //      });
                    //});

                    //.UseUrls("https://*:443");//�ֶ�ָ��https

                })
            //���Serilog��־�м��
            .AddSerilog();
    }
}
