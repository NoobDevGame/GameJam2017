using NoobFight.Contract.Simulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class CreateWorldResponseMessage : NetworkMessage
    {
        public override MessageType DataType => MessageType.CreateWorldResponse;

        public string WorldName { get; set; }
        public GameMode Mode { get; set; }

        public CreateWorldResponseMessage(string worldName, GameMode mode)
        {
            WorldName = worldName;
            Mode = mode;
        }

        public CreateWorldResponseMessage(CreateWorldRequestMessage request)
        {
            WorldName = request.WorldName;
            Mode = request.Mode;
        }

        public CreateWorldResponseMessage()
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