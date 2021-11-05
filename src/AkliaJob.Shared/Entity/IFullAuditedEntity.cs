using System;
using System.Collections.Generic;
using System.Text;

namespace AkliaJob.Shared
{
    /// <summary>
    /// 通用实体接口,包含逻辑删除
    /// </summary>
    public interface IFullAuditedEntity : ICreatedAudited, IModifiedAudited, ISoftDelete
    {

    }
}
