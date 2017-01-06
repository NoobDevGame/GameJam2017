using NoobFight.Contract.Entities;

namespace NoobFight.Core.Entities
{
    public class Player : Entity , IPlayer
    {
        public Player(string name, string textureName) : base(name, textureName)
        {
        }
    }
}