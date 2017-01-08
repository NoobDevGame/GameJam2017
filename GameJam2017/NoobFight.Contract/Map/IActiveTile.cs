using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Contract.Map
{
    public interface IActiveTile
    {
        RectangleF Region { get; }

        void OnAttack(IWorldManipulator manipulator, IEntity entity);

        void OnClick(IWorldManipulator manipulator, IEntity entity);

        void OnCollision(IWorldManipulator manipulator, IEntity entity);
    }
}
