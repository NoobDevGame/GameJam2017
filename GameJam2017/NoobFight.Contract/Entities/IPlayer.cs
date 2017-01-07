﻿using System;

namespace NoobFight.Contract.Entities
{
    public interface IPlayer : ICharacter
    {
        Guid ID { get; }

        Input Input { get; set; }
    }
}