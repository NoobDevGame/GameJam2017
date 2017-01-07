using NoobFight.Contract.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Contract.Map
{
    public interface IActiveTile
    {
        Vector2 Position { get; }

        void OnAttack(IEntity entity);

        void OnClick(IEntity entity);

        void OnCollision(IEntity entity);
    }
}
