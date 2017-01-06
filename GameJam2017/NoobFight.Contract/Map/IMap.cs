using System.Collections.Generic;

namespace NoobFight.Contract.Map
{
    public interface IMap
    {
        int Id { get; }
        ICollection<IArea> Areas { get; }
    }
}
