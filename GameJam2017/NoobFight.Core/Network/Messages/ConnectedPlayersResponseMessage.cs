using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class ConnectedPlayersResponseMessage : NetworkMessage
    {
        public ConnectedPlayersResponseMessage(int count) : base(4, null)
        {
            byte[] payload = new byte[4];
            payload = BitConverter.GetBytes(count);
            Payload = payload;
        }

        public ConnectedPlayersResponseMessage(byte[] data) : base(4, data)
        {

        }
    }
}
