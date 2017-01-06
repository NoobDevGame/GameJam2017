using System.Collections.Generic;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Map;
using NoobFight.Contract.Simulation;
using NoobFight.Core.Entities;

namespace NoobFight.Core.Simulation
{
    public class World : IWorld
    {
        public IMap Map { get; }

        private List<Entity> _entities = new List<Entity>();
        public IEnumerable<IEntity> Entities => _entities;


    }
}