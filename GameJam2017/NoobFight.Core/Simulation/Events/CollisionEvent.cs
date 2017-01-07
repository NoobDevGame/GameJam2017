using NoobFight.Contract.Entities;
using NoobFight.Contract.Map;
using NoobFight.Contract.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Core.Simulation.Events
{
    class CollisionEvent : WorldEvent
    {
        private IEntity entity;
        private IActiveTile tile;

        public override void Dispatch(IWorld world, ISimulation simulation)
        {
            tile.OnCollision(world.CreateNewManipulator() ,entity);
        }

        public CollisionEvent(IActiveTile BlockType, IEntity entity)
        {
            tile = BlockType;
            this.entity = entity;
        }
    }
}
