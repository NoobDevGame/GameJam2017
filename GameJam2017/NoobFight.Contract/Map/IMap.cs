using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Contract.Map
{
    public interface IMap
    {
        IArea StartArea { get; }

        IEnumerable<IArea> Areas { get; }

        void AddArea(IArea area);
    }
}
