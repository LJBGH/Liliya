using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Eventbus
{
    /// <summary>
    /// 事件基类
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// 事件唯一Id
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// 事件触发时间
        /// </summary>
        DateTime Timestamp { get; }
    }
}
