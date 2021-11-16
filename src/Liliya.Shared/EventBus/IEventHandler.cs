using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Liliya.Shared
{
    /// <summary>
    /// 事件处理器
    /// </summary>
    public interface IEventHandler    
    {
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="event"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> HandleAsync(IEvent @event, CancellationToken cancellationToken = default);


        bool CanHandle(IEvent @event);
    }

    public interface IEventHandler<in T> : IEventHandler where T : IEvent
    {
        Task<bool> HandleAsync(T @event, CancellationToken cancellationToken = default);
    }
}
