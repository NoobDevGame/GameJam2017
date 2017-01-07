using engenious;
using NoobFight.Core.Network;
using System;
using System.Collections.Generic;
using NoobFight.Core.Network.Messages;

namespace NoobFight.Components
{
    public class NetworkComponent : GameComponent
    {
        public new NoobFight Game { get; private set; }

        private Client client;
        private Dictionary<byte, Action<Client, NetworkMessage>> messageHandlers;

        public NetworkComponent(NoobFight game) : base(game)
        {
            Game = game;
            client = new Client();
            messageHandlers = new Dictionary<byte, Action<Client, NetworkMessage>>();
            RegisterMessageHandler<PongMessage>(2, (client, message) => Console.WriteLine("PONG"));
        }

        public void Connect(string host, int port)
        {
            client.Connect(host, port);
            client.OnMessageReceived += Client_OnMessageReceived;
            client.BeginReceive();
        }
        
        public void Disconnect()
        {
            client.Disconnect();
        }

        public void RegisterMessageHandler<T>(byte id, Action<Client, T> handler) where T : NetworkMessage
        {
            messageHandlers.Add(id, (Client c, NetworkMessage msg) => handler(c, (T)msg));
        }

        public void SendMessage(NetworkMessage message)
        {
            client.writeStream(message);
        }

        private void Client_OnMessageReceived(object sender, NetworkMessage message)
        {
            Action<Client, NetworkMessage> handler;
            if (messageHandlers.TryGetValue(message.DataType, out handler))
                handler((Client)sender, message);

        }
    }
}
