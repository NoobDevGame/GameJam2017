using NoobFight.Core.Entities;
using NoobFight.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Entities
{

    public class RemotePlayer : Player
    {
        public Client Client { get; private set; }
        public RemotePlayer(Client client, string name, string textureName) : base(client.ID, name, textureName)
        {
            Client = client;
        }
    }
}
