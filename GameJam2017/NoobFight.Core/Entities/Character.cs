using NoobFight.Contract.Entities;

namespace NoobFight.Core.Entities
{
    public class Character : Entity, ICharacter
    {
        public int Health { get; set; }

        public Character(string name, string textureName) : base(name, textureName)
        {
        }
    }
}