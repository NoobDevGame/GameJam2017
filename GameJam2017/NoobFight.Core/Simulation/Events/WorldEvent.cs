using NoobFight.Contract.Simulation;
using System;

namespace NoobFight.Core.Simulation.Events
{
    public abstract class WorldEvent : IWorldEvent
    {
        public abstract WorldEventType EventType { get;}

        public abstract SimulationMode SimulationMode { get; }

        public abstract bool ShareMode { get; }

        public abstract void Dispatch(IWorld world, ISimulation simulation);

        public abstract byte[] Serialize();

        public abstract void Deserialize(byte[] payload);

        public virtual void Refill(IWorld world)
        {

        }
    }
}