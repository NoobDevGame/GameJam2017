using System;

namespace NoobFight.Contract.Entities
{
    public interface IPlayer : ICharacter
    {
        long ID { get; }

        Input Input { get; set; }
    }
}