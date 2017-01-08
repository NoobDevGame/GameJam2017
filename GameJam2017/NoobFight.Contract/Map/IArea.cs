using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoobFight.Contract.Entities;

namespace NoobFight.Contract.Map
{
    public interface IArea
    {
        string Name { get; }
        int Width { get; }
        int Height { get; }
        Vector2 SpawnPoint { get; }

        IEnumerable<ILayer> Layers { get; }
        IEnumerable<IEntity> Entities { get; }

        List<IActiveTile> ActiveTiles { get; }

        void AddEntity(IEntity entity);
        void RemoveEntity(IEntity entity);

        MapTexture GetMapTextures(int id);
        Dictionary<string, MapTexture> MapTextures { get; } //TODO enumerable
        bool IsCellBlocked(int x, int y);
    }
}
