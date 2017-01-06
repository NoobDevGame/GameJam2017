using NoobFight.Contract;
using NoobFight.Contract.Entities;

namespace NoobFight.Core.Entities
{
    public class Entity : IEntity
    {
        public Entity(string name, string textureName)
        {
            Name = name;
            TextureName = textureName;
        }

        public string Name { get; private set; }
        public string TextureName { get; private set;}
        public Position Position { get; set; }
    }
}