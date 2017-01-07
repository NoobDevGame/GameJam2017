using System;
using System.Collections.Generic;
using NoobFight.Contract.Map;

namespace NoobFight.Core.Map
{
    public class Map : IMap
    {
        public IArea StartArea { get; private set; }

        private List<IArea> _areas = new List<IArea>();
        public IEnumerable<IArea> Areas => _areas;

        public void AddArea(IArea area)
        {
            if (StartArea == null)
                StartArea = area;
            this._areas.Add(area);
        }


    }
}