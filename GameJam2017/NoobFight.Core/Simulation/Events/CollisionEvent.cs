using NoobFight.Contract.Entities;
using NoobFight.Contract.Map;
using NoobFight.Contract.Simulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Simulation.Events
{
    class CollisionEvent : WorldEvent
    {
        private IEntity entity;
        private IActiveTile tile;

        public override WorldEventType EventType => WorldEventType.Collision;

        public override SimulationMode SimulationMode => SimulationMode.Server;

        public override bool ShareMode => false;

        public override void Dispatch(IWorld world, ISimulation simulation)
        {
            tile.OnCollision(world.CreateNewManipulator(), entity);
        }
        public CollisionEvent()
        {

        }
        public CollisionEvent(IActiveTile BlockType, IEntity entity)
        {
            tile = BlockType;
            this.entity = entity;
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
            using (MemoryStream ms = new MemoryStream(payload))
            using (BinaryReader br = new BinaryReader(ms))
            {

            }
        }
    }
}
