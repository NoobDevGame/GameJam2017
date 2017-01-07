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
        public override byte DataType => 1;

        public override string ToString()
        {
            return "Ping";
        }
    }
    public class PongMessage : NetworkMessage
    {
        public override byte DataType => 2;

        public override string ToString()
        {
            return "Pong";
        }
    }
}
