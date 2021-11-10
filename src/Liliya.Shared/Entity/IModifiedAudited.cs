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
        Guid? LastModifyId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        DateTime LastModifedAt { get; set; }
    }
}
