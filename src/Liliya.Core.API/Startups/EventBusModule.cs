using Liliya.Core.API.Event;
using Liliya.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liliya.Core.API.Startups
{
    /// <summary>
    /// 事件总线模块
    /// </summary>
    public static class EventBusModule
    {
        //自定义事件注入
        public static void AddEventBus(this IServiceCollection services) 
        {
            //事件总线注入
            services.AddTransient<IEventHandler, TestEventHander>();
            services.AddSingleton<IEventBus, EventBus>();
        }


        /// <summary>
        /// 事件总线配置
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseEventBus(this IApplicationBuilder app) 
        {
            //监听事件总线
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe();
            return app;
        }
    }
}
