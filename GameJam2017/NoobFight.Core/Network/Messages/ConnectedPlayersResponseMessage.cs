using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class ConnectedPlayersResponseMessage : NetworkMessage
    {
        public int Count { get; private set; }
        public override MessageType DataType => MessageType.ConnectedPlayersResponse;

        public ConnectedPlayersResponseMessage()
        {

        }
        public ConnectedPlayersResponseMessage(int count)
        {
            Count = count;
        }

        public override void Deserialize(byte[] payload)
        {
            Count = BitConverter.ToInt32(payload, 0);
        }

        public override byte[] Serialize()
        {
            return BitConverter.GetBytes(Count);
        }
    }
}
