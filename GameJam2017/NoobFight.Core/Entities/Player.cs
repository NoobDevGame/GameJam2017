using System;
using NoobFight.Contract;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;

namespace NoobFight.Core.Entities
{
    public class Player : Character, IPlayer
    {
        public Input Input { get; set; }

        public long PlayerID { get; private set; }

        public Player(long id,string name, string textureName) : base(name)
        {
            PlayerID = id;
            Health = 100;
            TextureName = textureName;
        }

        public override void OnEntityCollision(IWorldManipulator manipulator, IEntity collidedEntity)
        {
            var item = ((Entity)collidedEntity);

            if (item.Name != "coin")
            {
                return;
            }


            item.CurrentArea.RemoveEntity(item);

            Score++;
        }
    }
}