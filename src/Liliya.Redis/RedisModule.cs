using CSRedis;
using Liliya.Redis.Cache;
using Liliya.Shared.AppSetting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Redis
{
    /// <summary>
    /// Redis模块注入
    /// </summary>
    public static class RedisModule 
    {

        /// <summary>
        /// 使用CRedis
        /// </summary>
        /// <param name="service"></param>
        public static void AddCRedis(this IServiceCollection service) 
        {
            var connection = Appsettings.app(new string[] { "Liliya", "Redis", "ConnectionString" });
            var csredis = new CSRedisClient(connection);
            RedisHelper.Initialization(csredis);
            service.AddSingleton<IRedisCache, RedisCache>();
        }


        /// <summary>
        /// 使用分布式Redis
        /// </summary>
        /// <param name="service"></param>
        public static void AddDistributeRedis(this IServiceCollection service)
        {
            service.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
                options.InstanceName = "liliya";
            });
        }
    }
}
