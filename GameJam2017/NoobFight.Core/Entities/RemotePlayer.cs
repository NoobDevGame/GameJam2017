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
        public RemotePlayer(Client client,long id, string name, string textureName) : base(id, name, textureName)
        {
            Client = client;
        }
    }
}
