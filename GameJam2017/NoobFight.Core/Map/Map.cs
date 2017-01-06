using NoobFight.Contract.Map;
using System.Collections.Generic;

namespace NoobFight.Core.Map
{
    class Map : IMap
    {
        public int Id { get; private set; }

        public ICollection<IArea> Areas { get; private set; }


        public Map(int Id, ICollection<IArea> Areas)
        {
            this.Id = Id;
            this.Areas = Areas;
        }
    }
}
