namespace NoobFight.Contract.Simulation
{
    public interface IWorldEvent
    {
        WorldEventType EventType { get; }

        SimulationMode SimulationMode { get; }
        bool ShareMode { get; }

        void Dispatch(IWorld world, ISimulation simulation);
        byte[] Serialize();
        void Deserialize(byte[] payload);
        void Refill(IWorld world);
    }
}