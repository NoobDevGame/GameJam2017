﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Contract.Map
{
    public interface ILayer
    {
       ITile[,] Tiles { get; }
    }
}
