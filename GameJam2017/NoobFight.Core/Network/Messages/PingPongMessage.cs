using NoobFight.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Server.Message
{
    public class PingMessage : NetworkMessage
    {
        public PingMessage() : base(1, null)
        {
        }
        public override string ToString()
        {
            return "Ping";
        }
    }
    public class PongMessage : NetworkMessage
    {
        public PongMessage() : base(2, null)
        {
        }
        public override string ToString()
        {
            return "Pong";
        }
    }
}
