﻿using System;
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
        ConcurrentDictionary<int, Client> clients;

        IWorld world;

        public MessageHandler MessageHandler { get; set; } = new MessageHandler();

        public Server()
        {
            listener = new TcpListener(IPAddress.Any, 667);
            clients = new ConcurrentDictionary<int, Client>();
        }
       
        public void Start()
        {
            MessageHandler.RegisterMessageHandler<PingMessage>(PingMessage_Received);
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
                client.OnMessageReceived += MessageHandler.OnMessageReceived;
            });
        }

        private void PingMessage_Received(Client client,PingMessage message)
        {
            client.writeStream(new PongMessage());
        }
        

    }
}
