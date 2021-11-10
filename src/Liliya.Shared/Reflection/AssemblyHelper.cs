using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Liliya.Shared
{
    /// <summary>
    /// 反射获取程序集
    /// </summary>
    public class AssemblyHelper
    {
        /// <summary>
        /// 获取项目程序集，排除所有的系统程序集(Microsoft.***、System.***等)、Nuget下载包
        /// </summary>
        /// <returns></returns>
        public static List<Assembly> GetAllAssemblies()
        {
            string[] filters =
            {
                "mscorlib",
                "netstandard",
                "dotnet",
                "api-ms-win-core",
                "runtime.",
                "System",
                "Microsoft",
                "Window",
            };
            List<Assembly> list = new List<Assembly>();
            var deps = DependencyContext.Default;
            //排除所有的系统程序集、Nuget下载包
            var libs = deps.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package" && !filters.Any(lib.Name.StartsWith));
            try
            {
                foreach (var lib in libs)
                {
                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                    list.Add(assembly);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        public static Type[] FindAll()
        {

            var ds = GetAllAssemblies();
            var dss = ds.SelectMany(x => x.GetTypes());

            return GetAllAssemblies().SelectMany(x => x.GetTypes()).ToArray();

        }

        public static Type[] FindAll(Func<Type, bool> predicate)
        {
            return FindAll().Where(predicate).ToArray();
        }
    }
}
