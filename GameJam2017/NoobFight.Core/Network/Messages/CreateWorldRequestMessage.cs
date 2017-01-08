using NoobFight.Contract.Simulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class CreateWorldRequestMessage : NetworkMessage
    {
        public override MessageType DataType => MessageType.CreateWorldRequest;

        public string WorldName { get; set; }
        public GameMode Mode { get; set; }

        public CreateWorldRequestMessage(string worldName, GameMode mode)
        {
            WorldName = worldName;
            Mode = mode;
        }

        public CreateWorldRequestMessage()
        {

        }

        public override byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                bw.Write((int)Mode);
                bw.Write(WorldName);
                return ms.ToArray();
            }
        }

        public override void Deserialize(byte[] payload)
        {
            using (MemoryStream ms = new MemoryStream(payload))
            using (BinaryReader br = new BinaryReader(ms))
            {
                Mode = (GameMode)br.ReadInt32();
                WorldName = br.ReadString();
            }
        }
    }
}
