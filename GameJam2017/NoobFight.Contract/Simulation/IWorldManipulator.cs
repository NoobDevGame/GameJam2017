using NoobFight.Contract.Entities;

namespace NoobFight.Contract.Simulation
{
    public interface IWorldManipulator
    {
        void AddEvent(IWorldEvent worldEvent);

        void AddServerEvent(IWorldEvent worldEvent);

        void ChangeArea(IPlayer player, string destinationArea);
        void RemoveEntity(IEntity collidedEntity);
    }
}