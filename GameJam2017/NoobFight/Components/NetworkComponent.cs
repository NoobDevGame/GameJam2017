using engenious;
using NoobFight.Core.Network;
using NoobFight.Server.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Components
{
    public class NetworkComponent : GameComponent
    {
        public new NoobFight Game { get; private set; }

        private Client client;

        private Dictionary<byte, Action<Client, NetworkMessage>> messageHandlers = new Dictionary<byte, Action<Client, NetworkMessage>>();

        public NetworkComponent(NoobFight game) : base(game)
        {
            Game = game;
            client = new Client();

            RegisterMessageHandler<PongMessage>(2, (client, message) => Console.WriteLine("PONG"));
        }

        public void Connect(string host, int port)
        {
            client.Connect(host, port);
            client.OnMessageReceived += Client_OnMessageReceived;
            client.BeginReceive();
        }

        private void Client_OnMessageReceived(object sender, NetworkMessage message)
        {
            Action<Client, NetworkMessage> handler;
            if (messageHandlers.TryGetValue(message.DataType, out handler))
                handler((Client)sender, message);

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


    }
}
