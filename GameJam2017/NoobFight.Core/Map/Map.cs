using System;
using System.Collections.Generic;
using NoobFight.Contract.Map;

namespace NoobFight.Core.Map
{
    public class Map : IMap
    {
        public IArea StartArea { get; private set; }        
        public IEnumerable<IArea> Areas => _areas;

        private List<IArea> _areas = new List<IArea>();

        public void AddArea(IArea area)
        {
            if (StartArea == null)
                StartArea = area;
            _areas.Add(area);
        }


    }
}