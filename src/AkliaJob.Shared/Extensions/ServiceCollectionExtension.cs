using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AkliaJob.Shared
{
    public static class ServiceCollectionExtension
    {

        /// <summary>
        /// 获取配置文件服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IConfiguration GetConfiguration(this IServiceCollection services)
        {
            return services.GetBuildService<IConfiguration>();
        }

        /// <summary>
        /// 得到注入的服务
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static TType GetBuildService<TType>(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            return provider.GetService<TType>();
        }



        /// <summary>
        /// 获取单例注册服务对象
        /// </summary>
        public static T GetSingletonInstanceOrNull<T>(this IServiceCollection services)
        {
            ServiceDescriptor descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(T) && d.Lifetime == ServiceLifetime.Singleton);

            if (descriptor?.ImplementationInstance != null)
            {
                return (T)descriptor.ImplementationInstance;
            }

            if (descriptor?.ImplementationFactory != null)
            {
                return (T)descriptor.ImplementationFactory.Invoke(null);
            }

            return default;
        }

        public static T GetSingletonInstance<T>(this IServiceCollection services)
        {
            var service = services.GetSingletonInstanceOrNull<T>();
            if (service == null)
            {
                throw new InvalidOperationException("找不到singleton服务: " + typeof(T).AssemblyQualifiedName);
            }

            return service;
        }
    }
}
