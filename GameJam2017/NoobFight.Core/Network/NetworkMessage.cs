using System;
using System.IO;

namespace NoobFight.Core.Network
{
    public class NetworkMessage
    {
        public int DataType { get; set; }
        public byte[] Bytes { get; set; }

        public NetworkMessage() { }
        public NetworkMessage(byte[] data)
        {
            fromBytes(data);
        }

        internal byte[] GetBytes()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(DataType);
                    writer.Write(Bytes.Length);
                    writer.Write(Bytes);
                }
                return stream.ToArray();
            }
        }

        private void fromBytes(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(stream))
                {
                    DataType = reader.ReadInt32();
                    Bytes = reader.ReadBytes(reader.ReadInt32());
                }
            }
        }

        public override string ToString()
        {
            return "Not Implemented Exception";
        }
    }
}