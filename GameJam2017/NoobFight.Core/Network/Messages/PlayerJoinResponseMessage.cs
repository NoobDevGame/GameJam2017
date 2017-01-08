using System;
using System.Collections.Generic;
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

        public PlayerJoinResponseMessage(long id, string nick)
        {
            Id = id;
            Nick = nick;
        }
        public override void Deserialize(byte[] payload)
        {
            Id = BitConverter.ToInt64(payload, 0);
            Nick = Encoding.UTF8.GetString(payload, sizeof(long), payload.Length - sizeof(long));
        }
        public override byte[] Serialize()
        {
            byte[] nickBuffer = Encoding.UTF8.GetBytes(Nick);
            byte[] buffer = new byte[sizeof(long) + nickBuffer.Length];
            Array.Copy(BitConverter.GetBytes(Id), buffer, sizeof(long));
            Array.Copy(nickBuffer, 0, buffer, sizeof(long), nickBuffer.Length);
            return buffer;
        }
    }
}
