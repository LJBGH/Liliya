using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.EventBus
{
    /// <summary>
    /// 定义事件处理器公共接口，所有的事件处理都要实现该接口
    /// </summary>/// <summary>
    public interface IEventHander
    {

    }

    /// <summary>
    /// 泛型事件处理器接口
    /// </summary>
    /// <typeparam name="TEventData"></typeparam>
    public interface IEventHander<TEventData> : IEventHander where TEventData : IEventData 
    {
        /// <summary>
        /// 事件处理器实现该方法来处理事件
        /// </summary>
        /// <param name="eventData"></param>
        void HandleEvent(TEventData eventData);
    }
}
