using System;
using System.Collections.Generic;
using NoobFight.Contract;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;
using NoobFight.Core.Entities;
using NoobFight.Core.Simulation.Components;

namespace NoobFight.Core.Simulation
{
    public class Simulation : ISimulation
    {
        private List<World> _worlds = new List<World>();
        public IEnumerable<IWorld> Worlds => _worlds;

        private List<IPlayer> _players = new List<IPlayer>();
        public IEnumerable<IPlayer> Players => _players;

        private List<SimulationComponent> _components= new List<SimulationComponent>();

        public Simulation()
        {
            _components.Add(new GravitySimulationComponent());
            _components.Add(new MoveSimulationComponent());
            _components.Add(new MoveSimulationComponent());
            _components.Add(new CollisionSimulationComponent());
        }

        public IWorld CreateNewWorld(GameMode mode)
        {
            World newworld = new World(mode,this    );
            _worlds.Add(newworld);

            return newworld;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var world in _worlds)
            {
                Update(world,gameTime);
            }
        }

        private void Update(World world,GameTime gameTime)
        {
            if (world.State == WorldState.Running)
                world.UpdateState(gameTime);

            if (world.State == WorldState.Ended)
            {
                return;
            }

            foreach (var component in _components)
            {
                component.SimulateWorld(world, gameTime);
            }

            world.UpdateEvents();
        }

        public IPlayer CreateLocalPlayer(string name)
        {
            Player player = new Player(Guid.NewGuid(), name,"test");
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
    }
}