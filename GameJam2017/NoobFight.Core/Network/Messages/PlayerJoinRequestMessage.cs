using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class PlayerJoinRequestMessage : NetworkMessage
    {
        public override MessageType DataType => MessageType.PlayerJoinRequest;
        public string WorldName { get; private set; }

        public PlayerJoinRequestMessage()
        {

        }

        public PlayerJoinRequestMessage(string worldName)
        {
            WorldName = worldName;
        }

        public override byte[] Serialize()
        {
            return Encoding.UTF8.GetBytes(WorldName);
        }

        public override void Deserialize(byte[] payload)
        {
            WorldName = Encoding.UTF8.GetString(payload);
        }

    }
}
