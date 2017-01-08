using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class PlayerLoginRequestMessage : NetworkMessage
    {
        public override MessageType DataType => MessageType.PlayerLoginRequest;
        public string Nick { get; private set; }
        public PlayerLoginRequestMessage(string nick)
        {
            Nick = nick;
        }

        public PlayerLoginRequestMessage()
        {

        }

        public override void Deserialize(byte[] payload)
        {
            int length = BitConverter.ToInt32(payload, 0);
            Nick = Encoding.UTF8.GetString(payload, 4, payload.Length-4);
        }
        public override byte[] Serialize()
        {
            if (Nick == null)
                return new byte[4];
            byte[] nickBuffer = Encoding.UTF8.GetBytes(Nick);
            byte[] buffer = new byte[nickBuffer.Length+sizeof(int)];
            Array.Copy(BitConverter.GetBytes(nickBuffer.Length), 0, buffer, 0, sizeof(int));
            Array.Copy(nickBuffer, 0, buffer, sizeof(int), nickBuffer.Length);
            return buffer;
        }
    }
    public class PlayerLoginResponseMessage : NetworkMessage
    {
        public override MessageType DataType => MessageType.PlayerLoginResponse;

        public long PlayerId { get; private set; }

        public PlayerLoginResponseMessage()
        {

        }



        public PlayerLoginResponseMessage(long playerId)
        {
            PlayerId = playerId;
            
        }

        public override byte[] Serialize()
        {
            return BitConverter.GetBytes(PlayerId);
        }
        public override void Deserialize(byte[] payload)
        {
            PlayerId = BitConverter.ToInt64(payload, 0);
        }
        
    }
}
