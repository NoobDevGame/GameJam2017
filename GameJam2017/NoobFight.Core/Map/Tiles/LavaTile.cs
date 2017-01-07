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
    class LavaTile : ActiveTile
    {
        public LavaTile(Vector2 position) : base(position)
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
