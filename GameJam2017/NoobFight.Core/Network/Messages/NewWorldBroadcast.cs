using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class NewWorldBroadcast : NetworkMessage
    {
        public string WorldName { get; private set; }

        public override MessageType DataType => MessageType.NewWorldBroadcast;

        public NewWorldBroadcast() { }

        public NewWorldBroadcast(string worldname) : base()
        {
            WorldName = worldname;
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
