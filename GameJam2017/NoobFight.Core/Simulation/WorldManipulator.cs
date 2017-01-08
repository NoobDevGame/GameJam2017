using System;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;
using NoobFight.Core.Simulation.Events;

namespace NoobFight.Core.Simulation
{
    class WorldManipulator : IWorldManipulator
    {
        private World _world;

        public WorldManipulator(World world)
        {
            _world = world;
        }

        public void AddEvent(IWorldEvent worldEvent)
        {
            this._world.AddEvent(worldEvent);
        }

        public void AddServerEvent(IWorldEvent worldEvent)
        {
            this._world.AddEvent(worldEvent,true);
        }

        public void ChangeArea(IPlayer player, string destinationArea)
        {
            AddEvent(new AreaChangedEvent(player, destinationArea));
        }

        public void RemoveEntity(IEntity entity)
        {
            AddEvent(new RemoveEntityEvent(entity));
        }
    }
}