using NoobFight.Core.Network.Messages;
using System;
using System.IO;

namespace NoobFight.Core.Network.Messages
{
    public abstract class NetworkMessage
    {
        public abstract MessageType DataType { get; }

        public virtual byte[] Serialize()
        {
            return null;
        }

        public virtual void Deserialize(byte[] payload)
        {

        }

        internal byte[] GetBytes()
        {
            var payload = Serialize();
            if (payload == null)
                return new byte[] { (byte)DataType };
            byte[] data = new byte[payload.Length +sizeof(byte)];
            data[0] = (byte)DataType;
            Array.Copy(payload, 0, data, sizeof(byte), payload.Length);
            return data;
        }
    }
}