using System;
using System.Collections.Generic;
using System.Text;

namespace AkliaJob.Shared
{
    /// <summary>
    /// 创建人和创建时间
    /// </summary>
    public interface ICreatedAudited
    {
        /// <summary>
        /// 创建人Id
        /// </summary>
        Guid CreatedId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreatedAt { get; set; }
    }
}
