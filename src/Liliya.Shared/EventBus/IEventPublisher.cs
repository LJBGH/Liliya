using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Liliya.Shared
{
    /// <summary>
    /// 事件派发器
    /// </summary>
    public interface IEventPublisher : IDisposable
    {
        Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)where TEvent : IEvent;
    }
}
