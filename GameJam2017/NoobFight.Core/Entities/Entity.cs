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
            Radius = 0.5f;
            Height = 1f;
        }

        public string Name { get; private set; }
        public string TextureName { get; private set;}

        public float Mass { get; set; }


        public IArea CurrentArea { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Move { get; set; }
        public Vector2 Velocity { get; set; }

        public float Radius { get; set; }
        public float Height { get; set; }
    }
}