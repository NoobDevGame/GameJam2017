﻿using System.Collections.Generic;

namespace NoobFight.Contract.Map
{
    public interface IArea
    {
        int Width { get; }
        int Height { get; }
        ICollection<ILayer> Layers { get; }
    }
}
