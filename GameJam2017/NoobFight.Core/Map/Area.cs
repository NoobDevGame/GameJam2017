using System;
using System.Collections.Generic;
using System.Linq;
using NoobFight.Contract;
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

            MapTextures = new Dictionary<string, MapTexture>();
        }

        public int Width { get; }
        public int Height { get; }

        private Layer[] _layers;
        public IEnumerable<ILayer> Layers => _layers;

        private List<IEntity> _entities = new List<IEntity>();
        public IEnumerable<IEntity> Entities => _entities;

        public Dictionary<string, MapTexture> MapTextures { get; private set; }

        public Vector2 SpawnPoint { get; set; }

        public Vector2 SpawnPoint{get;set;}

        public void SetLayers(Layer[] layers)
        {
            _layers = layers;

        }

        public MapTexture GetMapTextures(int id)
        {
            return MapTextures.First(i => i.Value.Firstgid <= id && id <= i.Value.Firstgid + i.Value.Tilecount).Value;
        }

        public void AddEntity(IEntity entity)
        {
            entity.CurrentArea = this;
            _entities.Add(entity);
        }

        public void RemoveEntity(IEntity entity)
        {
            entity.CurrentArea = null;
            _entities.Remove(entity);
        }

        public bool IsCellBlocked(int x, int y)
        {
            if (x < 0 || y < 0)
                return true;
            if (x >= Width || y >= Height)
                return true;

            for (int i = 0; i < _layers.Length; i++)
            {
                var layer = _layers[i];
                var tilenumber = x + y * Width;
                var tileid = layer.Tiles[tilenumber];

                if (tileid == 0)
                    continue;


                var maptexture = GetMapTextures(tileid);
                var property = maptexture.GetTileProperty(tileid - maptexture.Firstgid);

                if (property.blocked)
                    return true;

            }

            return false;
        }
    }
}