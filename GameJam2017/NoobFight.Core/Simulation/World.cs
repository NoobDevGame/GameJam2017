using System;
using System.Collections.Generic;
using NoobFight.Contract;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Map;
using NoobFight.Contract.Simulation;
using NoobFight.Core.Entities;
using NoobFight.Core.Simulation.Events;
using System.Linq;

namespace NoobFight.Core.Simulation
{
    public class World : IWorld
    {
        public IMap CurrentMap { get; private set; }
        public GameMode Mode { get; private set; }
        public WorldState State { get; private set; }
        public TimeSpan WorldTime { get; private set; }
        public IWorldManipulator Manipulator { get; }
        public IEnumerable<IPlayer> Players => _players;
        public String Name { get; private set; } = "Default";

        public Action<IWorld,IWorldEvent> AddEventCallback { get; set; }

        private Queue<IWorldEvent> _events;
        private Simulation _simulation;
        private readonly List<IPlayer> _players;

        public World(GameMode mode, Simulation simulation, string name)
        {
            Mode = mode;
            _simulation = simulation;
            State = WorldState.Loaded;
            Manipulator = CreateNewManipulator();
            _events = new Queue<IWorldEvent>();
            _players = new List<IPlayer>();
            Name = name;
        }

        public void Start(IMap map)
        {
            CurrentMap = map;
            State = WorldState.Running;

            foreach (var player in Players)
            {
                CurrentMap.SetId(player);
                player.Position = CurrentMap.StartArea.SpawnPoint;
                AddEvent(new AreaChangedEvent(player, CurrentMap.StartArea.Name));
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
                    @event.Dispatch(this, _simulation);
                }
            }
        }

        public void AddEvent(IWorldEvent @event,bool bypass = false)
        {
            lock (_events)
            {
                if (bypass ||_simulation.Mode == SimulationMode.Single || _simulation.Mode == @event.SimulationMode)
                {
                    _events.Enqueue(@event);

                    if (@event.ShareMode)
                    {
                        AddEventCallback?.Invoke(this, @event);
                    }
                    
                }
            }
        }

        public void AddPlayer(IPlayer player)
        {
            _players.Add(player);
        }

        public void RemovePlayer(IPlayer player)
        {
            _players.Remove(player);
        }

        public IWorldManipulator CreateNewManipulator()
        {
            return new WorldManipulator(this);
        }

        public IPlayer FindPlayerById(long playerid)
        {
            return _players.First(i => i.PlayerID == playerid);
        }

        public void Resume()
        {
            State = WorldState.Running;
        }

        public IEntity FindEntityById(int id)
        {
            return CurrentMap.GetEntityById(id);
        }
    }
}