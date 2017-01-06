using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Contract.Map
{
    public interface IArea
    {
        int Width { get; }
        int Height { get; }
        IEnumerable<ILayer> Layers { get; }
    }
}
