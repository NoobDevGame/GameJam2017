using System;
using NoobFight.Contract;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Map;

namespace NoobFight.Core.Entities
{
    public class Entity : IEntity
    {
        public string Name { get; private set; }
        public EntityType Type { get; private set; }
        public string TextureName { get; set;}


        public IArea CurrentArea { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Move { get; set; }
        public Vector2 Velocity { get; set; }

        public float Radius { get; set; }
        public float Height { get; set; }
        public bool OnGround { get; set; }

        public Entity(string name, EntityType type)
        {
            Name = name;
            Type = type;
            TextureName = name;
            Radius = 0.5f;
            Height = 1f;
            Position = new Vector2(1, 1);
        }
    }
}