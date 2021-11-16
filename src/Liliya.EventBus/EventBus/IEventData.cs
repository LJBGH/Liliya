using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.EventBus
{
    public interface IEventData
    {
        /// <summary>
        /// 时间发生的时间
        /// </summary>
        DateTime EventTime { get; set; }

        /// <summary>
        /// 时间发生的对象
        /// </summary>
        object EventSource { get; set; }

    }
}
