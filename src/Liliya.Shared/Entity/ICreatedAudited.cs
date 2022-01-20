using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Shared
{
    /// <summary>
    /// 创建人和创建时间
    /// </summary>
    public interface ICreatedAudited
    {
        /// <summary>
        /// 创建人Id
        /// </summary>
        Guid CreateUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }
    }
}
