using System;

namespace NoobFight.Core.Network.Messages
{
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
