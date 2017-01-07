using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class PongMessage : NetworkMessage
    {
        public override byte DataType => 2;

        public override string ToString()
        {
            return "Pong";
        }
    }
}
