using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class PongMessage : NetworkMessage
    {
        public override MessageType DataType => MessageType.Pong;

        public override string ToString()
        {
            return "Pong";
        }
    }
}
