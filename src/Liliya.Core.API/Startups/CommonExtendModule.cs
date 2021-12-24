using Liliya.Shared;
using Microsoft.Extensions.DependencyInjection;
using Liliya.Swagger;
using Microsoft.AspNetCore.Builder;
using Liliya.AutoMapper;
using Liliya.SqlSugar.Repository;
using Microsoft.Extensions.Configuration;
using Liliya.Redis;
using System;
using Liliya.WebSockets;
using Liliya.WebSockets.SocketDictionary;

namespace Liliya.Core.API.Startups
{

    public static class CommonExtendModule
    {
        /// <summary>
        /// 公共拓展模块注入
        /// </summary>
        public static void AddCommonService(this IServiceCollection service, IConfiguration configuration) 
        {
            //swagger注入
            service.AddSwaggerService();
            //授权认证注入
            service.AddAuthService(configuration);
            //AutoMapper注入
            service.AddAutoMapperService();
            //SqlSugar泛型仓储注入
            service.AddScoped(typeof(ISqlSugarRepository<>), typeof(SqlSugarRepository<>));
            ////CRedis注入
            //service.AddCRedis();
            //使用分布式缓存
            service.AddDistributeRedis();
            //事件总线注入
            service.AddEventBus();

            //WebSocket服务注入
            service.AddSingleton<IWebSocketDictionary, WebSocketDictionary>();
        }





        //公共中间件
        public static IApplicationBuilder UseCommonExtension(this IApplicationBuilder app) 
        {
            //Consul服务注册发现配置
            //app.UseConsul();

            //Swagger配置
            app.UseSwaggerService();

            //使用事件总线
            app.UseEventBus();

            //自定义异常中间件
            app.CustomerMiddleware();

            //使用静态文件
            app.UseStaticFiles();



            //配置WebSocket中间件
            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
            };
            app.UseWebSockets(webSocketOptions);
            app.UseMiddleware<WebSocketHandlerMiddleware>();


            return app;
        }

    }
}
