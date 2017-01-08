using System;
using System.Collections.Generic;
using NoobFight.Contract;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;
using NoobFight.Core.Entities;
using NoobFight.Core.Simulation.Components;
using NoobFight.Core.Simulation.Events;

namespace NoobFight.Core.Simulation
{
    public class Simulation : ISimulation
    {
        public SimulationMode Mode { get; private set; }
        private List<World> _worlds = new List<World>();
        public IEnumerable<IWorld> Worlds => _worlds;
        private List<IPlayer> _players = new List<IPlayer>();
        public IEnumerable<IPlayer> Players => _players;

        private List<SimulationComponent> _components;

        public Simulation(SimulationMode mode)
        {
            Mode = mode;
            
            _components = new List<SimulationComponent>();
            _worlds = new List<World>();
            _players = new List<IPlayer>();

            _components.Add(new GravitySimulationComponent());
            if (Mode == SimulationMode.Lokal || Mode == SimulationMode.Single)
                _components.Add(new InputSimulationComponent());


            _components.Add(new MoveSimulationComponent());

            _components.Add(new CollisionSimulationComponent());
            _components.Add(new TileCollisionSimulationComponent());
            _components.Add(new EntityCollisionComponent());
        }

        public IWorld CreateNewWorld(GameMode mode, string name)
        {
            World newworld = new World(mode, this, name);
            _worlds.Add(newworld);

            return newworld;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var world in _worlds)
            {
                Update(world, gameTime);
            }
        }

        public IPlayer CreateLocalPlayer(long id, string name, string textureName)
        {
            Player player = new Player(1,name, textureName);
            InsertPlayer(player);

            return player;
        }

        public void InsertPlayer(IPlayer player)
        {
            _players.Add(player);
        }

        public void RemovePlayer(IPlayer player)
        {
            _players.Remove(player);
        }

        private void Update(World world, GameTime gameTime)
        {
            if (world.State == WorldState.Running)
                world.UpdateState(gameTime);

            if (world.State == WorldState.Ended)
            {
                return;
            }

            if (world.State == WorldState.Running)
            {
                foreach (var component in _components)
                {
                    component.SimulateWorld(world, gameTime);
                }

                world.UpdateEvents();
            }


        }
    }
}