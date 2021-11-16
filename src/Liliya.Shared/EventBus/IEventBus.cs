using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Shared
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public interface IEventBus : IEventPublisher, IEventSubscriber
    {

    }
}
