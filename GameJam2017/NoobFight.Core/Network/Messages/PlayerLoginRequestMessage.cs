using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class PlayerLoginRequestMessage : NetworkMessage
    {
        public override MessageType DataType => MessageType.PlayerLoginRequest;
        public string Nick { get; private set; }
        public string TextureName { get; private set; }
        public PlayerLoginRequestMessage(string nick, string textureName)
        {
            Nick = nick;
            TextureName = textureName;
        }

        public PlayerLoginRequestMessage()
        {

        }

        public override void Deserialize(byte[] payload)
        {
            using (MemoryStream ms = new MemoryStream(payload))
            using (BinaryReader br = new BinaryReader(ms))
            {
                Nick = br.ReadString();
                TextureName = br.ReadString();
            }
        }
        public override byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                bw.Write(Nick);
                bw.Write(TextureName);
                return ms.ToArray();
            }
        }
    }
}
