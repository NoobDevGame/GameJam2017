using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoobFight.Contract.Simulation;
using NoobFight.Contract.Entities;
using System.IO;

namespace NoobFight.Core.Simulation.Events
{
    public class RemoveEntityEvent : WorldEvent
    {
        public override WorldEventType EventType => WorldEventType.RemoveEntity;

        public override bool ShareMode => true;

        public override SimulationMode SimulationMode => SimulationMode.Server;

        public IEntity Entity { get; private  set; }

        private int _entityId;

        public RemoveEntityEvent(IEntity entity)
        {
            Entity = entity;
        }

        public RemoveEntityEvent()
        {

        }

        public override void Dispatch(IWorld world, ISimulation simulation)
        {
            if (Entity is IPlayer)
                return;

            Entity.CurrentArea.RemoveEntity(Entity);
        }

        public override void Refill(IWorld world)
        {
            Entity = world.FindEntityById(_entityId);
        }

        public override void Deserialize(byte[] payload)
        {
            using (MemoryStream ms = new MemoryStream(payload))
            using (BinaryReader br = new BinaryReader(ms))
            {
                _entityId = br.ReadInt32();
            }
        }

       

        public override byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                bw.Write(Entity.Id.HasValue ? Entity.Id.Value : -1);
                return ms.ToArray();
            }
        }
    }
}
