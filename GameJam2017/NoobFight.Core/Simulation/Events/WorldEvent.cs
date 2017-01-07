using NoobFight.Contract.Simulation;
using System;

namespace NoobFight.Core.Simulation.Events
{
    public abstract class WorldEvent
    {
        public abstract void Dispatch(World world,Simulation simulation);
    }
}