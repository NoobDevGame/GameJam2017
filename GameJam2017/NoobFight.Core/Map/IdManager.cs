using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoobFight.Contract;
using NoobFight.Contract.Simulation;
using NoobFight.Contract.Entities;
using System.Collections.Concurrent;

namespace NoobFight.Core.Map
{
    public class IdManager
    {
        public int[] Ids => entitys.Keys.ToArray();
        public IEntity[] Entitys => entitys.Values.ToArray();
        public List<KeyValuePair<int, IEntity>> ListedEntitys => entitys.ToList();

        ConcurrentDictionary<int, IEntity> entitys;

        public IdManager()
        {
            entitys = new ConcurrentDictionary<int, IEntity>();
        }

        public void SetId(IEntity entity)
        {
            if (entity == null)
                return;

            var id = entitys.Count + 1;

            while (!entitys.TryAdd(id, entity))
                id = entitys.Count + 1;

            entity.SetId(id);
        }

        public bool TryUpdateId(int newId, IEntity entity)
        {
            var returnValue = entitys.TryRemove(entity.Id.Value, out entity);
            returnValue = entitys.TryAdd(newId, entity);
            entity.SetId(newId);
            return returnValue;
        }

        public bool TryRemoveId(IEntity entity) => TryRemoveId(entity.Id.Value, entity);
        public bool TryRemoveId(int id) => TryRemoveId(id, default(IEntity));
        private bool TryRemoveId(int id, IEntity entity) => entitys.TryRemove(entity.Id.Value, out entity);

        public bool TryGetEntity(int id, out IEntity entity) => entitys.TryGetValue(id, out entity);
    }
}
