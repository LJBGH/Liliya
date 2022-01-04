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
                .UseServiceProviderFactory(new AutofacServiceProviderFactory()) //添加autofac服务,将内置容器替换为autofac容器
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    //绑定SSL证书
                    //.ConfigureKestrel(options =>
                    //{
                    //    options.Listen(System.Net.IPAddress.Any, 443, listenOptions =>
                    //      {
                    //          listenOptions.UseHttps(Path.Combine(AppContext.BaseDirectory, "liliya.work.pfx"), "134116");
                    //      });
                    //});

                    //.UseUrls("https://*:443");//手动指定https

                })
            //添加Serilog日志中间件
            .AddSerilog();
    }
}
