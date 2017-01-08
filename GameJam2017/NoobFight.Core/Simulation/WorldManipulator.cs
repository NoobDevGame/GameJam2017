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

        public void AddPlayer(IPlayer player)
        {
            var @event = new PlayerWorldEvent()
            {
                //PlayerId = player.Id.Value,
                Method = PlayerEventMethod.Insert,
            };
            _world.AddEvent(@event);

        }

        public void RemovePlayer(IPlayer player)
        {
            var @event = new PlayerWorldEvent()
            {
                PlayerId = player.Id.Value,
                Method = PlayerEventMethod.Remove,
            };
            _world.AddEvent(@event);
        }

        public void AddEvent(IWorldEvent worldEvent)
        {
            this._world.AddEvent(worldEvent);
        }

        public void ChangeArea(IPlayer player, string destinationArea)
        {
            AddEvent(new AreaChangedEvent(player, destinationArea));
        }
    }
}