using System;
using System.Collections.Generic;
using NoobFight.Contract.Map;
using NoobFight.Contract.Entities;

namespace NoobFight.Core.Map
{
    public class Map : IMap
    {
        public string Name { get; private set; }
        public IdManager IdManager { get; private set; }

        public IArea StartArea { get; private set; }
        public IEnumerable<IArea> Areas => _areas;

        private List<IArea> _areas = new List<IArea>();

        public Map(string name)
        {
            Name = name;
            IdManager = new IdManager();
        }

        public void Load()
        {
            AddArea(LoadArea("test"));
            AddArea(LoadArea("1test"));
        }

        private IArea LoadArea(string value) => MapLoader.LoadArea(value, IdManager);

        private void AddArea(IArea area)
        {
            if (StartArea == null)
                StartArea = area;
            _areas.Add(area);
        }

        public void SetId(IEntity entity)
        {
            IdManager.SetId(entity);
        }

        public IEntity GetEntityById(int id)
        {
            IEntity entity;
            if (!IdManager.TryGetEntity(id,out entity))
            {
                return null;
            }

            return entity;
        }
    }
}