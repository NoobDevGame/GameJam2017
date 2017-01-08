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
    class EntityCollisionEvent : WorldEvent
    {
        private IEntity collidingEntity;
        private IEntity collidedEntity;

        public override WorldEventType EventType => WorldEventType.EntityCollision;

        public override SimulationMode SimulationMode => SimulationMode.Server;

        public override bool ShareMode => false;
        public EntityCollisionEvent()
        {

        }
        public override void Dispatch(IWorld world, ISimulation simulation)
        {
            this.collidingEntity.OnEntityCollision(world.CreateNewManipulator(), collidedEntity);
        }

        public override byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                return ms.ToArray();
            }
        }

        public override void Deserialize(byte[] payload)
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryReader br = new BinaryReader(ms))
            {
            }
        }

        public EntityCollisionEvent(IEntity collidingEntity, IEntity collidedEntity)
        {
            this.collidedEntity = collidedEntity;
            this.collidingEntity = collidingEntity;
        }
    }
}
