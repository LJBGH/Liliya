using System;
using System.Collections.Generic;
using System.Text;

namespace AkliaJob.Shared
{
    /// <summary>
    /// 如果使用AutoMapper会跟官方冲突，所以在前面加了项目代号
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AkliaAutoMapperAttribute  : Attribute
    {
        public AkliaAutoMapperAttribute(params Type[] targetTypes)
        {
            targetTypes.NotNull(nameof(targetTypes));
            TargetTypes = targetTypes;
        }

        /// <summary>
        /// 类型数组
        /// </summary>
        public Type[] TargetTypes { get; private set; }

        public virtual AkliaAutoMapDirection MapDirection
        {
            get { return AkliaAutoMapDirection.From | AkliaAutoMapDirection.To; }
        }
    }
}
