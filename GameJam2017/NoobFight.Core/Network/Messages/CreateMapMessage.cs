using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class CreateMapMessage : NetworkMessage
    {
        public string MapName { get; set; }

        public override MessageType DataType => MessageType.CreateMapRequest;

        public CreateMapMessage(string mapname)
        {
            MapName = mapname;
        }

        public CreateMapMessage()
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
