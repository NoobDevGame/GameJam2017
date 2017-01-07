using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoobFight.Contract;
using NoobFight.Contract.Entities;

namespace NoobFight.Core.Map.Tiles
{
    class PortalTile : ActiveTile
    {
        public PortalTile(Vector2 position) : base(position)
        {
        }

        public override void OnCollision(IEntity entity)
        {

        }
    }
}
