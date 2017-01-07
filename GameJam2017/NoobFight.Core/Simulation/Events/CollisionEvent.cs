using NoobFight.Contract.Entities;
using NoobFight.Contract.Map;
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

        public override void Dispatch(World world, Simulation simulation)
        {
            tile.OnCollision(entity);
        }

        public CollisionEvent(IActiveTile BlockType, IEntity entity)
        {
            tile = BlockType;
            this.entity = entity;
        }
    }
}
