using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Shared
{
    public interface IModifiedAudited
    {
        /// <summary>
        /// 最后修改人Id
        /// </summary>
        Guid? LastModifyUserId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        DateTime LastModifyTime { get; set; }
    }
}
