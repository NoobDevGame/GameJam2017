using System.IO;
using Newtonsoft.Json;
using NoobFight.Contract.Map;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace NoobFight.Core.Map
{
    public class MapLoader
    {

        private class FileMap
        {
            public string Name { get; set; }
            public FileArea[] Areas { get; set; }

            public IMap ToMap()
            {
                return null;
            }
        }

        private class FileArea
        {
            public string Name { get; set; }

            public int  Height { get; set; }

            public int Width { get; set; }

            public FileLayer Layers { get; set; }

            public IArea ToArea()
            {
                
            }
        }

        private class FileLayer
        {
            public int ID { get; set; }

            public FileTile[] Tiles { get; set; }

            public ILayer ToLayer()
            {
                var layer = new Layer();
                layer.Id = this.ID;

                ITile[] tiles = new ITile[Tiles.Length];
                for (int i = 0; i < tiles.Length; i++)
                {
                    tiles[i] = Tiles[i].ToTile();
                }

                return layer;
            }
        }

        private class FileTile
        {
            public int X { get; set; }

            public int Y { get; set; }

            public int Asset { get; set; }

            public bool Collidable { get; set; }

            public ITile ToTile()
            {
                return new Tile(this.Asset, this.Collidable, this.X, this.Y);
            }
        }

        private string _Path;

        public MapLoader(string Path = null)
        {
            this._Path = GetPath(Path);
        }

        public void CreateTestMap()
        {
            FileMap testmap = new FileMap()
            {
                Name = "TestMap"
            };

            testmap.Areas = new FileArea[] { new FileArea { Name="HalloArea", Height = 5, Width = 5 } };
            testmap.
            SaveMap(testmap);
        }

        private void SaveMap(FileMap map)
        {
            File.WriteAllText(this._Path, JsonConvert.SerializeObject(map));
        }

        public IMap LoadMap()
        {
            return JsonConvert.DeserializeObject<FileMap>(File.ReadAllText(this._Path)).ToMap();
        }

        private string GetPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return path = Path.Combine(Directory.GetCurrentDirectory(), "NoobFightMap");
            else
                return path;
        }
    }
}
