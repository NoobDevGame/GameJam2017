using System;
using NoobFight.Contract;
using NoobFight.Contract.Entities;

namespace NoobFight.Core.Entities
{
    public class Player : Character , IPlayer
    {
        public Guid ID { get; private set; }
        public Input Input { get; set; }

        public Player(Guid id, string name, string textureName) : base(name)
        {
            ID = id;
            Health = 100;
            TextureName = textureName;
        }


    }
}