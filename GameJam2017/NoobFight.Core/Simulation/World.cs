using System;
using System.Collections.Generic;
using NoobFight.Contract;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Map;
using NoobFight.Contract.Simulation;
using NoobFight.Core.Entities;
using NoobFight.Core.Simulation.Events;

namespace NoobFight.Core.Simulation
{
    public class World : IWorld
    {
        private Queue<WorldEvent> _events = new Queue<WorldEvent>();

        private Simulation _simulation;

        public World(GameMode mode, Simulation simulation)
        {
            Mode = mode;
            _simulation = simulation;
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

        public void UpdateEvents()
        {
            lock (_events)
            {
                while (_events.Count > 0)
                {
                    var @event = _events.Dequeue();
                    @event.Dispatch(this,_simulation);
                }
            }
        }

        public void AddPlayer(IPlayer player)
        {
            lock (_events)
            {

            }
        }

        public void RemovePlayer(IPlayer player)
        {
            lock (_events)
            {

            }

        }
    }
}