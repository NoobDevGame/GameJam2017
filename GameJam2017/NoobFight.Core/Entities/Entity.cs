using NoobFight.Contract;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Map;

namespace NoobFight.Core.Entities
{
    public class Entity : IEntity
    {
        public Entity(string name, string textureName)
        {
            Name = name;
            TextureName = textureName;
            Mass = 50f;
        }

        public string Name { get; private set; }
        public string TextureName { get; private set;}

        public float Mass { get; set; }

        public Position Position { get; set; }
        public IArea CurrentArea { get; set; }
    }
}