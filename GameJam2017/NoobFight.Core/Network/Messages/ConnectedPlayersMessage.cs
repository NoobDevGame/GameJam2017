using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class ConnectedPlayersRequestMessage : NetworkMessage
    {
        public ConnectedPlayersRequestMessage() : base(3,null)
        {
        }
        public ConnectedPlayersRequestMessage(byte[] data) : base(3,data) 
        {
        }
    }
}
