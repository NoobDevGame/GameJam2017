using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class StartWorldMessage : NetworkMessage
    {
        public string MapName { get; set; }

        public override MessageType DataType => MessageType.StartWorld;

        public StartWorldMessage(string mapname)
        {
            MapName = mapname;
        }

        public StartWorldMessage()
        {

        }

        public override byte[] Serialize()
        {
            return Encoding.UTF8.GetBytes(MapName);
        }

        public override void Deserialize(byte[] payload)
        {
            MapName = Encoding.UTF8.GetString(payload);
        }
    }
}
