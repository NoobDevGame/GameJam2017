using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NoobFight.Contract;
using NoobFight.Contract.Map;
using NoobFight.Core.Entities;
using NoobFight.Contract.Entities;
using NoobFight.Core.Map.Tiles;

namespace NoobFight.Core.Map
{
    public static class MapLoader
    {
        #region FileObject

        private class FileArea
        {
            public int height { get; set; }
            public int width { get; set; }

            public int tileheight { get; set; }
            public int tilewidth { get; set; }

            public FileLayer[] layers { get; set; }
            public FileTileSets[] tilesets { get; set; }
        }



        private class FileTileSets
        {
            public int firstgid { get; set; }
            public string image { get; set; }

            public int spacing { get; set; }

            public int tilecount { get; set; }

            public int tileheight { get; set; }
            public int tilewidth { get; set; }

            public int columns { get; set; }

            public Dictionary<int, FileTileProperty> tileproperties { get; set; }
        }

        private class FileTileProperty
        {
            public bool collisionevent { get; set; }
            public bool blocked { get; set; }
            public int entitytype { get; set; }

            public TileProperty GetTileProperty()
            {
                TileProperty property = new TileProperty();
                property.blocked = blocked;
                property.collisionevent = collisionevent;

                return property;
            }
        }

        private class FileLayer
        {
            public int height { get; set; }
            public int width { get; set; }

            public string name { get; set; }

            public bool visible { get; set; }

            public string type { get; set; }

            public int[] data { get; set; }

            public FileObject[] objects { get; set; }
        }

        private class FileObject
        {
            public float height { get; set; }

            public float width { get; set; }

            public float x { get; set; }

            public float y { get; set; }

            public string type { get; set; }

            public string name { get; set; }

            public FileObjectProperty properties { get; set; }
        }

        private class FileObjectProperty
        {
            public bool collisionevent { get; set; }
            public int entitytype { get; set; }
            public string @class { get; set; }
        }

        #endregion

        public static Area LoadArea(string name)
        {
            var path = Path.Combine("Content", "Maps", $"{name}.json");
            if (File.Exists(path))
            {
                using (var fs = File.OpenRead(path))
                using (var sr = new StreamReader(fs))
                {
                    var mapjson = sr.ReadToEnd();
                    var mapobject = JsonConvert.DeserializeObject<FileArea>(mapjson);
                    var map = Convert(mapobject);
                    return map;
                }
            }

            return null;
        }

        static Dictionary<string, Type> ActiveTiles = new Dictionary<string, Type>();


        private static Area Convert(FileArea fa)
        {

            ActiveTiles.Add("Lava", typeof(LavaTile));

            Area area = new Area(fa.width, fa.height);
            area.ActiveTiles = new List<IActiveTile>();

            List<Layer> layers = new List<Layer>();

            for (int l = 0; l < fa.layers.Length; l++)
            {
                var fl = fa.layers[l];

                if (fl.type == "objectgroup")
                {
                    //fl.name == "EventLayer";
                    //fl.name == "EntityLayer";

                    foreach (FileObject fileObject in fl.objects)
                    {
                        Vector2 position = new Vector2(fileObject.x / fa.tilewidth, fileObject.y / fa.tileheight);
                        Vector2 size = new Vector2(fileObject.width / fa.tilewidth, fileObject.height / fa.tileheight);

                        if (fl.name == "EventLayer")
                        {
                            if (ActiveTiles.TryGetValue(fileObject.type, out var tileType))
                            {
                                for (var x = position.X; x < position.X + size.X; x++)
                                {
                                    for (var y = position.Y; y < position.Y + size.Y; y++)
                                    {
                                        var obj = (ActiveTile)Activator.CreateInstance(tileType, position);
                                        area.ActiveTiles.Add(obj);
                                    }
                                }


                            }
                        }


                        if (fileObject.type == "spawn")
                        {
                            var startposition = new Vector2(position.X + size.X / 2, position.Y + size.Y);
                            area.SpawnPoint = startposition;
                        }
                        else if (fileObject.type == "entity")
                        {
                            area.AddEntity(new Entity(fileObject.name, EntityType.Item)
                            {
                                Height = size.Y,
                                Radius = size.X / 2,
                                Position = position + new Vector2(size.X / 2, size.Y)
                            });
                        }
                    }
                }
                else
                {
                    layers.Add(new Layer(l, fl.data));
                }
            }

            area.SetLayers(layers.ToArray());


            for (int t = 0; t < fa.tilesets.Length; t++)
            {
                var ft = fa.tilesets[t];
                FileInfo textureinfo = new FileInfo(ft.image);

                var index = textureinfo.Name.LastIndexOf(".", StringComparison.Ordinal);
                string key = textureinfo.Name;

                if (index != -1)
                    key = key.Remove(index);

                var contenttexture = new MapTexture(key, ft.firstgid, ft.tilecount, ft.spacing, ft.tileheight, ft.tilewidth, ft.columns);

                foreach (var tile in ft.tileproperties)
                {
                    contenttexture.SetTileProperty(tile.Key, tile.Value.GetTileProperty());
                }

                area.MapTextures.Add(key, contenttexture);


            }

            return area;
        }

    }
}