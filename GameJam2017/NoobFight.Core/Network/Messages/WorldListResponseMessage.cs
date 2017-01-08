using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class WorldListResponseMessage : NetworkMessage
    {
        public string[] Worlds { get; set; }
        public override MessageType DataType => MessageType.WorldListResponseMessage;

        public WorldListResponseMessage()
        {

        }
        public WorldListResponseMessage(string[] worlds)
        {
            Worlds = worlds;
        }

        public override void Deserialize(byte[] payload)
        {
            var length = payload[0];
            Worlds = new string[length];
            int index = 0;
            for(int i = 1; i < payload.Length; i++)
            {
                var len = payload[i];

                Worlds[index++] = Encoding.UTF8.GetString(payload, i + 1, len);

                i += len;
            }
        }

        public override byte[] Serialize()
        {
            using (var mStream = new MemoryStream())
            {
                mStream.WriteByte((byte)Worlds.Length);
                foreach(var world in Worlds)
                {
                    byte[] sBytes = Encoding.UTF8.GetBytes(world);
                    mStream.Write(BitConverter.GetBytes((byte)sBytes.Length), 0, sizeof(byte));
                    mStream.Write(sBytes, 0, sBytes.Length);
                }
                return mStream.ToArray();
            }

        }
    }
}
