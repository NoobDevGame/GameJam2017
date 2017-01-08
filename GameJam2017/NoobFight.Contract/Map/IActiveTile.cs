using System.Drawing;
using NoobFight.Core.Map;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;

namespace NoobFight.Contract.Map
{
    public interface IActiveTile
    {
        RectangleF Region { get; }
        TileProperty Property { get; }

        void OnAttack(IWorldManipulator manipulator, IEntity entity);

        void OnClick(IWorldManipulator manipulator, IEntity entity);

        void OnCollision(IWorldManipulator manipulator, IEntity entity);
    }
}
