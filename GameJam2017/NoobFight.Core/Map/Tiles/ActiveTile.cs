using NoobFight.Contract.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoobFight.Contract.Entities;
using NoobFight.Contract;
using NoobFight.Core.Simulation;

namespace NoobFight.Core.Map.Tiles
{
    public abstract class ActiveTile : IActiveTile
    {
        public World World { get; private set; }

        public Vector2 Position { get; private set; }

        public ActiveTile(Vector2 position)
        {
            this.Position = position;
        }

        public virtual void OnAttack(IEntity entity)
        {

        }

        public virtual void OnClick(IEntity entity)
        {

        }

        public virtual void OnCollision(IEntity entity)
        {

        }
    }
}
