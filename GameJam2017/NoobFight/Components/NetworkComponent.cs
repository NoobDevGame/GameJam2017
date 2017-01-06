using engenious;
using NoobFight.Core.Network;
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
        
        public NetworkComponent(NoobFight game) : base(game)
        {
            Game = game;
            client = new Client();
        }

        public void Connect(string host, int port)
        {
            client.Connect(host, port);
        }

        public void Disconnect()
        {
            client.Disconnect();
        }


    }
}
