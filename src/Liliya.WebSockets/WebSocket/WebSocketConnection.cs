using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

namespace Liliya.WebSockets
{
    public class WebSocketConnection
    {
        //声明一个SingleClass类的变量来引用唯一的对象
        private static WebSocketConnection instance = null;

        //声明一个私有的构造方法，让外部无法调用这个类的构造方法
        private WebSocketConnection () {  }

        //锁同步
        private static readonly object obj = new object();

        //创建静态的方法，  创建此类唯一的对象
        public static WebSocketConnection Instance 
        {

            get 
            {
                if (instance == null) 
                {
                    lock (obj)
                    {
                        //双重锁定
                        if (instance == null)
                        {
                            instance = new WebSocketConnection();       //调用私有的构造方法，创建实例
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 客户端连接池
        /// </summary>
        public Dictionary<string, WebSocketClient> WebsocketClientCollection { get; set; } = new Dictionary<string, WebSocketClient>();
    }

    public class WebSocketClient 
    {
        public List<WebSocket> Clients { get; set; }
    }
}
