using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    class PlayerJoinMessage : NetworkMessage
    {
        public override MessageType DataType => MessageType.PlayerJoin;
        public PlayerJoinMessage()
        {

        }
    }
}
