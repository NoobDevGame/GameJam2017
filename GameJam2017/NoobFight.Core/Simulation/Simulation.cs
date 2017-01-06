using System.Collections.Generic;
using NoobFight.Contract.Simulation;

namespace NoobFight.Core.Simulation
{
    public class Simulation : ISimulation
    {
        private List<World> _worlds = new List<World>();
        public IEnumerable<IWorld> Worlds => _worlds;

        public IWorld CreateNewWorld(GameMode mode)
        {
            World newworld = new World(mode);
            _worlds.Add(newworld);

            return newworld;

        }

        public void Update()
        {
            foreach (var world in _worlds)
            {

            }
        }
    }
}