using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Liliya.WebSockets.SocketDictionary
{
    public interface IWebSocketDictionary
    {

        /// <summary>
        /// 添加一个WebSocket客户端
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        bool AddSocketClient(string userId, WebSocket client);

        /// <summary>
        /// 移除一个客户端连接
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        bool RemoveSocketClient(string userId, WebSocket client);


        /// <summary>
        /// 向所有客户端发送消息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<bool> SendMessageToAllClient(string message);

        /// <summary>
        /// 向指定用户客户端发送消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<bool> SendMessageToAssignClient(string userId, string message);

    }
}
