using Liliya.WebSockets.SocketDictionary;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace  Liliya.WebSockets
{
    /// <summary>
    /// WebSocket中间件拓展
    /// </summary>
    public class WebSocketHandlerMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly IWebSocketDictionary _webSocketDictionary;
        public WebSocketHandlerMiddleware(RequestDelegate next, IWebSocketDictionary webSocketDictionary)
        {
            _next = next;
            _webSocketDictionary = webSocketDictionary;
        }

        public async Task Invoke(HttpContext context) 
        {

            //判断是否是ws请求
            if (context.Request.Path == "/ws")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    //创建Socket连接
                    using (WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync())
                    {
                        Console.WriteLine("创建链接客户端:" + context.Connection.Id);
                        //await Handle(Guid.NewGuid().ToString(), webSocket, webSocketDictionary);
                        var userId = context.User.Claims.Where(x => x.Type == "jti").FirstOrDefault();
                        if (userId != null)
                        {
                            await Handle(userId.Value, webSocket, _webSocketDictionary);
                        }
                        else
                        {
                            await Handle(context.Connection.Id, webSocket, _webSocketDictionary);
                        }
                    }
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                }
            }
            else
            {
                await _next(context);
            }
        }

        /// <summary>
        /// 等待客户端消息
        /// </summary>
        /// <param name="userId"></用户Id>
        /// <param name="client"></客户端>
        /// <param name="webSocketDictionary"></param>
        /// <returns></returns>
        private async Task Handle(string clientId, WebSocket client, IWebSocketDictionary webSocketDictionary)
        {
            try
            {
                //添加到连接池
                webSocketDictionary.AddSocketClient(clientId, client);
                var buffer = new byte[1024 * 4];
                WebSocketReceiveResult result = null;
                while (client.State != WebSocketState.Closed && client.State != WebSocketState.Aborted)
                {
                    result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    //判断客户端是否断开连接
                    if (client.State == WebSocketState.CloseReceived && result.MessageType == WebSocketMessageType.Close)
                    {
                        await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                        webSocketDictionary.RemoveSocketClient(clientId, client);
                        Console.WriteLine("客户端已关闭");
                    }

                    //客户端如果为打开状态则读取消息
                    if (client.State == WebSocketState.Open)
                    {
                        //读取文本数据
                        if (result.MessageType == WebSocketMessageType.Text)
                        {
                            var msg = Encoding.UTF8.GetString(buffer).Trim('\0');

                            if (msg == "ping") 
                            {                      
                                Console.WriteLine("dassfdddddd");
                            }

                            Console.WriteLine($"收到客户端消息:{msg}");

                            //返回+消息给客户端
                            await SendStringAsync(client, "收到客户端消息: " + msg);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"客户端{clientId}出现错误:\n {e.Message}");
            }
            finally
            {
                if (client.State != WebSocketState.Closed)
                {
                    client.Abort();
                }
                if (webSocketDictionary.RemoveSocketClient(clientId, client))
                {
                    Console.WriteLine($"客户端:{clientId}移除");
                    client.Dispose();
                }
            }
        }

        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private Task SendStringAsync(WebSocket socket, string data, CancellationToken cancellationToken = default(CancellationToken))
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            return socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, cancellationToken);
        }
    }
}
