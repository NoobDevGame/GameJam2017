using System.Collections.Generic;
using NoobFight.Contract.Map;

namespace NoobFight.Core.Map
{
    public static class MapGenerator
    {
        private  static  Dictionary<string, IArea> _areas = new Dictionary<string, IArea>();
        public static IMap CreateMap()
        {
            Map map = new Map();
            map.AddArea(LoadArea("test"));

            return map;
        }

        private static IArea LoadArea(string name)
        {
            IArea area = null;

            if (!_areas.TryGetValue(name,out area))
            {
                area = MapLoader.LoadArea(name);
                _areas.Add(name,area);
            }

            return area;
        }
    }
}