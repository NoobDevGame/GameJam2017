using NoobFight.Contract.Map;
using System.Collections.Generic;

namespace NoobFight.Core.Map
{
    class Area : IArea
    {
        public ICollection<ILayer> Layers { get; private set; }

        public int Height { get; private set; }

        public int Width { get; private set; }

        public Area(int Height, int Width, ICollection<ILayer> Layers)
        {
            this.Width = Width;
            this.Height = Height;
            this.Layers = Layers;
        }
    }
}
