using System;
using NoobFight.Contract.Entities;

namespace NoobFight.Core.Entities
{
    public class Player : Entity , IPlayer
    {
        public Guid ID { get; private set; }

        public Player(Guid id,string name, string textureName) : base(name, textureName)
        {
            ID = id;
        }


    }
}