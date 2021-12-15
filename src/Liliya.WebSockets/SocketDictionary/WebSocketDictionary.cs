using Liliya.Shared;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Liliya.WebSockets.SocketDictionary
{
    public class WebSocketDictionary : IWebSocketDictionary
    {

        /// <summary>
        /// 添加一个客户端
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool AddSocketClient(string userId, WebSocket client)
        {
            if (userId.IsNull() || client.IsNull())
                return false;

            if (WebSocketConnection.Instance.WebsocketClientCollection.ContainsKey(userId))
            {
                WebSocketConnection.Instance.WebsocketClientCollection[userId].Clients.Add(client);

            }
            else
            {
                WebSocketClient webSocket = new WebSocketClient
                {
                    Clients = new List<WebSocket> { client }
                };

                WebSocketConnection.Instance.WebsocketClientCollection.Add(userId, webSocket);
            }
            return true;
        }

        /// <summary>
        /// 关闭一个客户端连接
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool RemoveSocketClient(string userId, WebSocket client)
        {
            if (WebSocketConnection.Instance.WebsocketClientCollection.ContainsKey(userId))
            {
                if (WebSocketConnection.Instance.WebsocketClientCollection[userId].Clients.Count > 1)
                {
                    WebSocketConnection.Instance.WebsocketClientCollection[userId].Clients.Remove(client);
                }
                else
                {
                    WebSocketConnection.Instance.WebsocketClientCollection.Remove(userId);
                }
                return true;
            }
            return false;
        }


        /// <summary>
        /// 向所有客户端发送消息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<bool> SendMessageToAllClient(string message)
        {
            byte[] buffer = System.Text.Encoding.Default.GetBytes(message);
            if (WebSocketConnection.Instance.WebsocketClientCollection.Count > 0)
            {
                foreach (var item in WebSocketConnection.Instance.WebsocketClientCollection)
                {
                    foreach (var client in item.Value.Clients)
                    {
                        if (client.State == WebSocketState.Open)
                        {
                            await client.SendAsync(
                            new ArraySegment<byte>(buffer, 0, buffer.Length),
                            WebSocketMessageType.Text,
                            true,
                            CancellationToken.None);
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 向指定用户的客户端发送消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<bool> SendMessageToAssignClient(string userId, string message)
        {
            byte[] buffer = System.Text.Encoding.Default.GetBytes(message);
            if (WebSocketConnection.Instance.WebsocketClientCollection.ContainsKey(userId))
            {
                var clients = WebSocketConnection.Instance.WebsocketClientCollection[userId].Clients;
                if (clients.Count > 0)
                {
                    foreach (var item in clients)
                    {
                        await item.SendAsync(
                           new ArraySegment<byte>(buffer, 0, buffer.Length),
                           WebSocketMessageType.Text,
                           true,
                           CancellationToken.None);
                    }
                }
                return true;
            }
            else
            {
                Console.WriteLine("该用户没有在客户端登录");
                return false;
            }
        }
    }
}
