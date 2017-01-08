using System;
using System.Collections.Generic;
using NoobFight.Contract.Map;

namespace NoobFight.Core.Map
{
    public class Map : IMap
    {
        public IArea StartArea { get; private set; }        
        public IEnumerable<IArea> Areas => _areas;

        private List<IArea> _areas;

        public void AddArea(IArea area)
        {
            _areas = new List<IArea>();

            if (StartArea == null)
                StartArea = area;
            _areas.Add(area);
        }


    }
}