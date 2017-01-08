using System;

namespace NoobFight.Contract.Entities
{
    public interface IPlayer : ICharacter
    {
        long PlayerID { get;  }

        Input Input { get; set; }
    }
}