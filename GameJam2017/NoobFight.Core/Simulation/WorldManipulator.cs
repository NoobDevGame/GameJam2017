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
                PlayerID = player.ID,
                Method = PlayerEventMethod.Insert,
            };
            _world.AddEvent(@event);

        }

        public void RemovePlayer(IPlayer player)
        {
            var @event = new PlayerWorldEvent()
            {
                PlayerID = player.ID,
                Method = PlayerEventMethod.Remove,
            };
            _world.AddEvent(@event);
        }
    }
}