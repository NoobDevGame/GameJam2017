﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoobFight.Contract.Map;

namespace NoobFight.Contract.Entities
{
    public interface IEntity
    {
        string Name { get; }
        EntityType Type { get; }

        string TextureName { get; }

        float Radius { get; set; }
        float Height { get; set; }
        
        IArea CurrentArea { get; set; }

        bool OnGround { get; set; }

        Vector2 Position { get; set; }
        Vector2 Move { get; set; }
        Vector2 Velocity { get; set; }
    }
}
