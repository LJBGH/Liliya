using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Shared
{
    /// <summary>
    /// 消息事件参数
    /// </summary>
    public class EventProcessedEventArgs : EventArgs
    {
        public IEvent Event { get; }

        public EventProcessedEventArgs(IEvent @event)
        {
            Event = @event;
        }
    }
}
