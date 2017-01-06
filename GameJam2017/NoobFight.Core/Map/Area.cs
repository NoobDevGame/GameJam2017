using System.Collections.Generic;
using NoobFight.Contract.Map;

namespace NoobFight.Core.Map
{
    public class Area : IArea
    {
        public Area(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }

        private List<Layer> _layers = new List<Layer>();
        public IEnumerable<ILayer> Layers => _layers;
    }
}