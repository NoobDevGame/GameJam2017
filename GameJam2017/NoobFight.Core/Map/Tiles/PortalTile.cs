using System.Drawing;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;

namespace NoobFight.Core.Map.Tiles
{
    class PortalTile : ActiveTile
    {
        public PortalTile(RectangleF region, TileProperty property) : base(region, property)
        {
        }

        public override void OnCollision(IWorldManipulator manipulator, IEntity entity)
        {

        }

        public override void OnClick(IWorldManipulator manipulator, IEntity entity)
        {
            if (entity is IPlayer)
            {
                if (!string.IsNullOrEmpty(this.Property.destinationarea))
                    manipulator.ChangeArea(((IPlayer)entity), this.Property.destinationarea);
            }
        }
    }
}
