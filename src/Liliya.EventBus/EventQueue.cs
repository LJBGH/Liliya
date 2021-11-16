using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Eventbus
{
    /// <summary>
    /// 消息队列,当消息推送进来的时候，立刻通知订阅方进行处理
    /// </summary>
    internal sealed class Eventbus
    {
        public event System.EventHandler<EventProcessedEventArgs> EventPushed;

        public EventQueue() { }

        public void Push(IEvent @event)
        {
            OnMessagePushed(new EventProcessedEventArgs(@event));
        }

        private void OnMessagePushed(EventProcessedEventArgs e) => this.EventPushed?.Invoke(this, e);
    }
}
