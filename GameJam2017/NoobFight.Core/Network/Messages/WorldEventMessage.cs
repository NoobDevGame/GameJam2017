using NoobFight.Contract.Simulation;
using NoobFight.Core.Simulation.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Network.Messages
{
    public class WorldEventMessage : NetworkMessage
    {
        public override MessageType DataType => MessageType.WorldEvent;

        public IWorldEvent WorldEvent { get; private set; }

        public WorldEventMessage()
        {

        }

        public WorldEventMessage(IWorldEvent worldEvent)
        {
            WorldEvent = worldEvent;
        }

        public override byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                bw.Write((int)WorldEvent.EventType);
                var data = WorldEvent.Serialize();
                bw.Write(data.Length);
                bw.Write(data);
                return ms.ToArray();
            }
        }

        public override void Deserialize(byte[] payload)
        {
            using (MemoryStream ms = new MemoryStream(payload))
            using (BinaryReader br = new BinaryReader(ms))
            {
                var type = (WorldEventType)br.ReadInt32();
                var length = br.ReadInt32();

                var data = br.ReadBytes(length);

                WorldEvent = EventManager.Deserialize(type, data);
            }
        }


    }
}
