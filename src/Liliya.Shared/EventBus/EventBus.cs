using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Liliya.Shared
{
    /// <summary>
    /// 事件总线处理
    /// </summary>
    public class EventBus : IEventBus
    {
        private readonly EventQueue eventQueue = new EventQueue();
        private readonly IEnumerable<IEventHandler> eventHandlers;

        public EventBus( IEnumerable<IEventHandler> eventHandlers)
        {
            this.eventHandlers = eventHandlers;
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent
        {
            await Task.Factory.StartNew(() => eventQueue.Push(@event));
        }

        private void EventQueue_EventPushed(object sender, EventProcessedEventArgs e)
            => (
            from eh in this.eventHandlers
            where eh.CanHandle(e.Event)
            select eh
            ).ToList()
            .ForEach(async eh => await eh.HandleAsync(e.Event));


        /// <summary>
        /// 订阅
        /// </summary>
        public void Subscribe()
        {
            eventQueue.EventPushed += EventQueue_EventPushed;
        }

        public void Dispose() => Dispose();


        private bool disposedValue = false; // To detect redundant calls
        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.eventQueue.EventPushed -= EventQueue_EventPushed;
                }
                disposedValue = true;
            }
        }
    }
}
