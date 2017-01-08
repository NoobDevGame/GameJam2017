using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoobFight.Contract.Simulation;
using NoobFight.Contract.Entities;

namespace NoobFight.Core.Simulation.Events
{
    class EntityCollisionEvent : WorldEvent
    {
        private IEntity collidingEntity;
        private IEntity collidedEntity;

        public override void Dispatch(IWorld world, ISimulation simulation)
        {
            this.collidingEntity.OnEntityCollision(world.CreateNewManipulator(), collidedEntity);
        }

        public EntityCollisionEvent(IEntity collidingEntity, IEntity collidedEntity)
        {
            this.collidedEntity = collidedEntity;
            this.collidingEntity = collidingEntity;
        }
    }
}
