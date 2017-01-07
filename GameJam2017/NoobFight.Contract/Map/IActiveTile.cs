using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;
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

        void OnAttack(IWorldManipulator manipulator, IEntity entity);

        void OnClick(IWorldManipulator manipulator, IEntity entity);

        void OnCollision(IWorldManipulator manipulator, IEntity entity);
    }
}
