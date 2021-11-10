using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Liliya.Shared
{
    public class AssemblyExtension
    {

        /// <summary>
        /// 反射获取程序集下的类信息
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static Type GetAssemblyClass(string assemblyName, string className) 
        {
            //var assemblysService = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("Liliya.Quertz")).FirstOrDefault();
            Assembly assembly = Assembly.Load(new AssemblyName(assemblyName));
            Type type = assembly.GetType(className);
            return type;
        }
    }
}
