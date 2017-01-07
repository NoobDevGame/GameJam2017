using System;
using NoobFight.Contract;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Map;

namespace NoobFight.Core.Entities
{
    public class Entity : IEntity
    {
        public Entity(string name, EntityType type)
        {
            this.Name = name;
            this.Type = type;
            this.TextureName = name;
            this.Radius = 0.5f;
            this.Height = 1f;
            this.Position = new Vector2(1, 1);
        }

        public string Name { get; private set; }
        public EntityType Type { get; private set; }
        public string TextureName { get; set; }

        public IArea CurrentArea { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Move { get; set; }
        public Vector2 Velocity { get; set; }

        public float Radius { get; set; }
        public float Height { get; set; }
        public bool OnGround { get; set; }
    }
}