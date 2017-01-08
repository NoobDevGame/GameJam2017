using NoobFight.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class PingMessage : NetworkMessage
    {
        public override MessageType DataType => MessageType.Ping;

        public override string ToString()
        {
            return "Ping";
        }
    }
    
}
