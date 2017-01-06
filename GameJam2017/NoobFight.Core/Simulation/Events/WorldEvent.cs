using System;

namespace NoobFight.Core.Simulation.Events
{
    public abstract class WorldEvent
    {
        public void Dispatch(World world, Simulation simulation)
        {
        }
    }
}