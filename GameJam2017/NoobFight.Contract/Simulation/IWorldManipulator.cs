using NoobFight.Contract.Entities;

namespace NoobFight.Contract.Simulation
{
    public interface IWorldManipulator
    {
        void AddPlayer(IPlayer player);
        void RemovePlayer(IPlayer player);
    }
}