using System;
using System.Collections.Generic;
using System.Text;
using WatsonWebsocket;

namespace Liliya.WebSockets
{
    public class WeebSocketModule
    {
        public void Test() 
        {
            WatsonWsServer server = new WatsonWsServer
            {

            };
            server.ClientConnected += ClientConnected;
            server.ClientDisconnected += ClientDisconnected;
            server.MessageReceived += MessageReceived;
            server.Start();

            static void ClientConnected(object sender, ClientConnectedEventArgs args)
            {
                Console.WriteLine("Client connected: " + args.IpPort);
            }

            static void ClientDisconnected(object sender, ClientDisconnectedEventArgs args)
            {
                Console.WriteLine("Client disconnected: " + args.IpPort);
            }

            static void MessageReceived(object sender, MessageReceivedEventArgs args)
            {
                Console.WriteLine("Message received from " + args.IpPort + ": " + Encoding.UTF8.GetString(args.Data));
            }
        }
    }
}
