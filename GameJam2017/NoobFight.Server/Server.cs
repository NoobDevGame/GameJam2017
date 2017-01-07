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
using NoobFight.Server.Message;

namespace NoobFight.Server
{
    internal class Server
    {
        TcpListener listener;
        ConcurrentDictionary<int, Client> clients;
        Simulation simulation;

        IWorld world;

        private Dictionary<byte, Action<Client,NetworkMessage>> messageHandlers = new Dictionary<byte, Action<Client,NetworkMessage>>();

        public Server()
        {
            listener = new TcpListener(IPAddress.Any, 667);
            clients = new ConcurrentDictionary<int, Client>();
            simulation = new Simulation();
        }
        private void RegisterMessageHandler<T>(byte id,Action<Client,T> handler) where T : NetworkMessage
        {
            messageHandlers.Add(id, (Client c,NetworkMessage msg) => handler(c,(T)msg));
        }
        public void Start()
        {
            RegisterMessageHandler<PingMessage>(1, PingMessage_Received);
            world = simulation.CreateNewWorld(GameMode.Timed);
            world.Start(MapGenerator.CreateMap());
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
                var client = new Client(tcpClient, clients.Count + 1);
                while (!added)
                    added = clients.TryAdd(client.ID, client);

                client.BeginReceive();
                client.OnMessageReceived += client_OnMessageReceived;
            });
        }
        private void PingMessage_Received(Client client,PingMessage message)
        {
            client.writeStream(new PongMessage());
            throw new NotSupportedException("blub I did it again >:" + message.ToString());
        }
        private void client_OnMessageReceived(object sender, NetworkMessage message)
        {
            Action<Client,NetworkMessage> handler;
            if (messageHandlers.TryGetValue(message.DataType, out handler))
                handler((Client)sender,message);

            throw new NotSupportedException("Uuups I did it again >:" + message.ToString());
        }

    }
}
