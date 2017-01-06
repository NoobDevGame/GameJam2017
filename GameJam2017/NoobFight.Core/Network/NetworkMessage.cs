using System;
using System.IO;

namespace NoobFight.Core.Network
{
    public class NetworkMessage
    {
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

                }
            }
        }

        public override string ToString()
        {
            return "Not Implemented Exception";
        }
    }
}