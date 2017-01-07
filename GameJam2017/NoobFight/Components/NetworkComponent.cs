using engenious;
using NoobFight.Core.Network;
using NoobFight.Core.Network.Messages;
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

        private MessageHandler messageHandler;

        public NetworkComponent(NoobFight game) : base(game)
        {
            Game = game;
            client = new Client();
            messageHandler = new MessageHandler();

            messageHandler.RegisterMessageHandler<PongMessage>((client, message) => Console.WriteLine("PONG"));
        }

        public void Connect(string host, int port)
        {
            client.Connect(host, port);
            client.OnMessageReceived += messageHandler.OnMessageReceived;
            client.BeginReceive();
        }

        public void Disconnect()
        {
            client.Disconnect();
        }

        public void SendMessage(NetworkMessage message)
        {
            client.writeStream(message);
        }


    }
}
