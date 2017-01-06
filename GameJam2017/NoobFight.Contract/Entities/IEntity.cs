﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Contract.Entities
{
    public interface IEntity
    {
        string Name { get; }
        string TextureName { get; }
        Position Position { get; set; }
    }
}