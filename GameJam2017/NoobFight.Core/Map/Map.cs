using System.Collections.Generic;
using NoobFight.Contract.Map;

namespace NoobFight.Core.Map
{
    public class Map : IMap
    {
        private List<Area> _areas =new List<Area>();
        public IEnumerable<IArea> Areas => _areas;
    }
}