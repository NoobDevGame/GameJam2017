using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class PlayerJoinResponseMessage : NetworkMessage
    {
        public override MessageType DataType => MessageType.PlayerJoinResponse;
        public PlayerJoinResponseMessage()
        {

        }
        public long Id { get; private set; }
        public string Nick { get; private set; }
        public string TextureName { get; set; }
        public PlayerJoinResponseMessage(long id, string nick, string textureName)
        {
            Id = id;
            Nick = nick;
            TextureName = textureName;
        }
        public override void Deserialize(byte[] payload)
        {
            using (MemoryStream ms = new MemoryStream(payload))
            using (BinaryReader br = new BinaryReader(ms))
            {
                Id = br.ReadInt64();
                Nick = br.ReadString();
                TextureName = br.ReadString();
            }
        }
        public override byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                bw.Write(Id);
                bw.Write(Nick);
                bw.Write(TextureName);
                return ms.ToArray();
            }
        }
    }
}
