using System;
using System.IO;

namespace NoobFight.Core.Network
{
    public class NetworkMessage
    {
        public byte DataType { get; set; }
        public byte[] Payload { get; set; }

        public NetworkMessage(byte dataType,byte[] payLoad)
        {
            DataType = dataType;
            Payload = payLoad;
        }

        internal byte[] GetBytes()
        {
            if (Payload == null)
                return new byte[] { DataType };
            byte[] data = new byte[Payload.Length +sizeof(byte)];
            data[0] = DataType;
            Array.Copy(Payload, 0, data, sizeof(byte), Payload.Length);
            return data;
            /*using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(DataType);
                    writer.Write(Payload.Length);
                    writer.Write(Payload);
                }
                return stream.ToArray();
            }*/
        }

        public override string ToString()
        {
            return "Not Implemented Exception";
        }
    }
}