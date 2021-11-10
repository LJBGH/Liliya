using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Shared
{
    /// <summary>
    /// 通用实体接口,不包含逻辑删除
    /// </summary>
    public interface IFullAuditedNoSoftEnyity : ICreatedAudited, IModifiedAudited
    {

    }
}
