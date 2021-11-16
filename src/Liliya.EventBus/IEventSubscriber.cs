﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Liliya.Eventbus
{
    /// <summary>
    /// 事件订阅器
    /// </summary>
    public interface IEventSubscriber : IDisposable
    {
        void Subscribe();
    }
}
