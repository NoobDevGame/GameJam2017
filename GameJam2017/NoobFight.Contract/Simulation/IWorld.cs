using NoobFight.Contract.Entities;
using NoobFight.Contract.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Contract.Simulation
{
    public interface IWorld
    {
        IMap Map { get; }

        ICollection<IEntity> Entities { get; }
    }
}
