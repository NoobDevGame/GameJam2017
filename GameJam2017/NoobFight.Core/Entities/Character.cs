using System.Collections.Generic;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Map;

namespace NoobFight.Core.Entities
{
    public class Character : Entity, ICharacter
    {
        public float InteractionRadius { get; set; }

        public List<IEntity> EntityInteractionList { get; private set; }

        public List<IActiveTile> TileInteractionList { get; private set; }

        public int Health { get; set; }

        public Character(string name) : base(name, EntityType.Character)
        {
            InteractionRadius = 1f;
            EntityInteractionList = new List<IEntity>();
            TileInteractionList = new List<IActiveTile>();
        }
    }
}