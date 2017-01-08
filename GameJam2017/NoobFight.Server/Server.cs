using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NoobFight.Core.Network;
using NoobFight.Core.Simulation;
using NoobFight.Contract.Simulation;
using NoobFight.Core.Map;
using NoobFight.Core.Network.Messages;

namespace NoobFight.Server
{
    internal class Server
    {
        TcpListener listener;
        ConcurrentDictionary<long, Client> clients;

        IWorld world;

        public MessageHandler MessageHandler { get; set; }

        public Server()
        {
            listener = new TcpListener(IPAddress.Any, 4344);
            clients = new ConcurrentDictionary<long, Client>();
            MessageHandler = new MessageHandler();
        }
       
        public void Start()
        {
            listener.Start();
            listener.BeginAcceptTcpClient(HandShake, null);
        }

        private void HandShake(IAsyncResult ar)
        {
            var tcpClient = listener.EndAcceptTcpClient(ar);
            listener.BeginAcceptTcpClient(HandShake, null);

            Task.Run(() =>
            {
                bool added = false;
                var client = new Client(tcpClient, BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0));
                while (!added)
                    added = clients.TryAdd(client.ID, client);

                client.OnMessageReceived += MessageHandler.OnMessageReceived;
                client.BeginReceive();
            });
        }

        internal void RegisterMessageHandler<T>()
        {
            throw new NotImplementedException();
        }

        public void RegisterMessageHandler<T>(Action<Client, T> handler) where T : NetworkMessage, new()
        {
            MessageHandler.RegisterMessageHandler(handler);
        }

        public void SendBroadcast(NetworkMessage message)
        {
            foreach(var client in clients)
            {
                client.Value.writeStream(message);
            }
        }

    }
}
