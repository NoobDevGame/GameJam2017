using System.Collections.Generic;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Map;
using NoobFight.Contract.Simulation;
using NoobFight.Core.Entities;

namespace NoobFight.Core.Simulation
{
    public class World : IWorld
    {
        public World(GameMode mode)
        {
            Mode = mode;
            State = WorldState.Loaded;
        }

        public IMap Map { get; }

        private List<Player> _players = new List<Player>();
        public IEnumerable<IPlayer> Players => _players;

        public GameMode Mode { get; }
        public WorldState State { get; }


    }
}