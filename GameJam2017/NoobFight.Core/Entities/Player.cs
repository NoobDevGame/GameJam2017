using System;
using NoobFight.Contract;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;

namespace NoobFight.Core.Entities
{
    public class Player : Character, IPlayer
    {
        public long ID { get; private set; }
        public Input Input { get; set; }

        public Player(long id, string name, string textureName) : base(name)
        {
            ID = id;
            Health = 100;
            TextureName = textureName;
        }

        public override void OnEntityCollision(IWorldManipulator manipulator, IEntity collidedEntity)
        {
            var item = ((Entity)collidedEntity);

            if (item.Name != "coin")
                return;

            item.CurrentArea.RemoveEntity(item);

            this.Health -= 1;
        }
    }
}