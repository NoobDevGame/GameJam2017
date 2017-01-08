using System.Drawing;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;

namespace NoobFight.Core.Map.Tiles
{
    class LavaTile : ActiveTile
    {
        public LavaTile(RectangleF region, TileProperty property) : base(region, property)
        {
        }

        public override void OnCollision(IWorldManipulator manipulator, IEntity entity)
        {
            if (entity is ICharacter)
            {
                ((ICharacter)entity).Health -= 1;
            }
        }
    }
}
