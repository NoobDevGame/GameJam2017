using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoobFight.Contract;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;
using System.Drawing;

namespace NoobFight.Core.Map.Tiles
{
    class PortalTile : ActiveTile
    {
        public PortalTile(RectangleF region) : base(region)
        {
        }

        public override void OnCollision(IWorldManipulator manipulator, IEntity entity)
        {
            if (entity is IPlayer)
            {
                manipulator.ChangeArea(((IPlayer)entity), "1test");
            }
        }
    }
}
