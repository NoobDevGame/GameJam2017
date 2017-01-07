using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class PongMessage : NetworkMessage
    {
        public PongMessage() : base(2, null)
        {
        }
        public PongMessage(byte[] data) : base(2, data)
        {
        }

        public override string ToString()
        {
            return "Pong";
        }
    }
}
