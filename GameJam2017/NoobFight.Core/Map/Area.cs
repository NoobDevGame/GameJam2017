using System.Collections.Generic;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Map;
using NoobFight.Core.Entities;

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

        private Layer[] _layers;
        public IEnumerable<ILayer> Layers => _layers;

        private List<Entity> _entities = new List<Entity>();
        public IEnumerable<IEntity> Entities => _entities;

        public Dictionary<string,MapTexture> MapTextures { get; private set; }

        public void SetLayers(Layer[] layers)
        {
            _layers = layers;
            MapTextures = new Dictionary<string, MapTexture>();
        }
    }
}