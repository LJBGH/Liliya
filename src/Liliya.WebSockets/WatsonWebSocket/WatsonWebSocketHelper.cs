using System;
using System.Collections.Generic;
using System.Text;
using WatsonWebsocket;

namespace Liliya.WebSockets.WatsonWebSocket
{
    /// <summary>
    /// WatsonWebSocket中间件
    /// </summary>
    public class WatsonWebSocketHelper
    { 
        private static WatsonWsServer  server = new WatsonWsServer("localhost", 5002, false);
        private static Dictionary<string, string> clients = new Dictionary<string, string>();
        /// <summary>
        /// 开启WebSocket服务
        /// </summary>
        public static void StartWatsonWebsocket()
        {
            server.ClientConnected += ClientConnected;
            server.ClientDisconnected += ClientDisconnected;
            server.MessageReceived += MessageReceived;
            server.Start();
            
        }


        /// <summary>
        /// 客户端连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        static void ClientConnected(object sender, ClientConnectedEventArgs args)
        {
            Console.WriteLine("Client connected: " + args.IpPort);           
            Console.WriteLine("IP地址:" + args.IpPort);
        }

        /// <summary>
        /// 客户端断开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        static void ClientDisconnected(object sender, ClientDisconnectedEventArgs args)
        {
            Console.WriteLine("Client disconnected: " + args.IpPort);
        }

        /// <summary>
        /// 接受客户端消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        static void MessageReceived(object sender, MessageReceivedEventArgs args)
        {
            Console.WriteLine("Message received from " + args.IpPort + ": " + Encoding.UTF8.GetString(args.Data));
        }



    }

}
