using System;
using System.Collections.Generic;
using NoobFight.Contract;
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

        public IMap CurrentMap { get; private set; }

        private List<IPlayer> _players = new List<IPlayer>();
        public IEnumerable<IPlayer> Players => _players;

        public GameMode Mode { get; private set; }
        public WorldState State { get; private set; }

        public TimeSpan WorldTime { get; private set; }

        public void Start(IMap map)
        {
            CurrentMap = map;
            State = WorldState.Running;
        }

        public void Pause()
        {
            State = WorldState.Paused;
        }

        public void UpdateState(GameTime gameTime)
        {
            WorldTime += gameTime.ElapsedTime;
        }

        public void AddPlayer(IPlayer player)
        {
            _players.Add(player);
        }

        public void RemovePlayer(IPlayer player)
        {
            _players.Remove(player);
        }
    }
}