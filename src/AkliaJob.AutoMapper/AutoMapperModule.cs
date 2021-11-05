using AkliaJob.Shared;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace AkliaJob.AutoMapper
{
    public static class AutoMapperModule
    {
        public static void AddAutoMapperService(this IServiceCollection service) 
        {

            //获取所有程序集
            var assemblys = AssemblyHelper.GetAllAssemblies();

            var suktAutoMapTypes = assemblys.SelectMany(x => x.GetTypes()).Where(s => s.IsClass && !s.IsAbstract && s.HasAttribute<AkliaAutoMapperAttribute>(true)).Distinct().ToArray();

            service.AddAutoMapper(mapper =>
            {
                CreateMapping<AkliaAutoMapperAttribute>(suktAutoMapTypes, mapper);
            },
            assemblys,
            ServiceLifetime.Singleton
            );
            var mapper = service.GetBuildService<IMapper>();//获取autoMapper实例
            AutoMapperHelper.SetMapper(mapper);
        }

        /// <summary>
        /// 创建扩展方法
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="sourceTypes"></param>
        /// <param name="mapperConfigurationExpression"></param>
        private static void CreateMapping<TAttribute>(Type[] sourceTypes, IMapperConfigurationExpression mapperConfigurationExpression) where TAttribute : AkliaAutoMapperAttribute
        {
            foreach (var sourceType in sourceTypes)
            {
                var attribute = sourceType.GetCustomAttribute<TAttribute>();
                if (attribute.TargetTypes?.Count() <= 0)
                {
                    return;
                }
                foreach (var tatgetType in attribute.TargetTypes)

                {
                    ///判断是To
                    if (attribute.MapDirection.HasFlag(AkliaAutoMapDirection.To))
                    {
                        mapperConfigurationExpression.CreateMap(sourceType, tatgetType);
                    }
                    ///判断是false
                    if (attribute.MapDirection.HasFlag(AkliaAutoMapDirection.From))
                    {
                        mapperConfigurationExpression.CreateMap(tatgetType, sourceType);
                    }
                }
            }
        }
    }
}
