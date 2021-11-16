using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.EventBus
{
    public class EventData : IEventData
    {
        public EventData()
        {
            EventTime = DateTime.Now;
        }

        /// <summary>
        /// 事件发生的时间
        /// </summary>
        public DateTime EventTime { get ; set ; }

        /// <summary>
        /// 触发事件的对象
        /// </summary>
        public object EventSource { get ; set ; }
    }
}
