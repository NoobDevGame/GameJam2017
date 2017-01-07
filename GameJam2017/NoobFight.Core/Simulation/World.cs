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
            Manipulator = CreateNewManipulator();
        }

        public IMap CurrentMap { get; private set; }

        private readonly List<IPlayer> _players = new List<IPlayer>();
        public IEnumerable<IPlayer> Players => _players;

        public GameMode Mode { get; private set; }
        public WorldState State { get; private set; }

        public TimeSpan WorldTime { get; private set; }

        public IWorldManipulator Manipulator { get; }

        public void Start(IMap map)
        {
            CurrentMap = map;
            State = WorldState.Running;

            foreach (var player in Players)
            {
                player.Position = CurrentMap.StartArea.SpawnPoint;
                CurrentMap.StartArea.AddEntity(player);
            }
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

        public void AddEvent(WorldEvent @event)
        {
            lock (_events)
            {
                _events.Enqueue(@event);
            }
        }

        public void AddPlayer(IPlayer player)
        {
            _players.Add(player);

            if (State == WorldState.Running || State == WorldState.Paused)
            {
                player.Position = CurrentMap.StartArea.SpawnPoint;
                CurrentMap.StartArea.AddEntity(player);
            }
        }

        public void RemovePlayer(IPlayer player)
        {
            if (State == WorldState.Running || State == WorldState.Paused)
            {
                player.CurrentArea?.RemoveEntity(player);
            }

            _players.Remove(player);
        }


        public IWorldManipulator CreateNewManipulator()
        {
            return new WorldManipulator(this);
        }
    }
}