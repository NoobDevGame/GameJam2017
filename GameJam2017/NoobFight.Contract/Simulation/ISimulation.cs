using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Contract.Simulation
{
    public interface ISimulation
    {
        IEnumerable<IWorld> Worlds { get; }

        IWorld CreateNewWorld();

        void Update();
    }
}
