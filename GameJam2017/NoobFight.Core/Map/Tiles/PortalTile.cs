using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoobFight.Contract;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;

namespace NoobFight.Core.Map.Tiles
{
    class PortalTile : ActiveTile
    {
        public PortalTile(Vector2 position) : base(position)
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
