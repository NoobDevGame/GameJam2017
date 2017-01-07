using NoobFight.Contract.Simulation;
using System;

namespace NoobFight.Core.Simulation.Events
{
    public abstract class WorldEvent : IWorldEvent
    {
        public abstract void Dispatch(IWorld world, ISimulation simulation);
    }
}