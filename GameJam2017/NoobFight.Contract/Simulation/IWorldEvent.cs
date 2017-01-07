namespace NoobFight.Contract.Simulation
{
    public interface IWorldEvent
    {
        void Dispatch(IWorld world, ISimulation simulation);
    }
}