using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

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
            public bool blocked { get; set; }

            public TileProperty GetTileProperty()
            {
                TileProperty property = new TileProperty();
                property.blocked = blocked;

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

        private static Area Convert(FileArea fa)
        {
            Area area = new Area(fa.width, fa.height);

            Layer[] layers = new Layer[fa.layers.Length];

            for (int l = 0; l < fa.layers.Length; l++)
            {
                var fl = fa.layers[l];

                layers[l] = new Layer(l, fl.data);

            }

            area.SetLayers(layers);


            for (int t = 0; t < fa.tilesets.Length; t++)
            {
                var ft = fa.tilesets[t];
                FileInfo textureinfo = new FileInfo(ft.image);

                var index = textureinfo.Name.LastIndexOf(".", StringComparison.Ordinal);
                string key = textureinfo.Name;

                if (index != -1)
                    key = key.Remove(index);

                var contenttexture = new MapTexture(key, ft.firstgid, ft.tilecount, ft.spacing, ft.tileheight, ft.tilewidth,ft.columns);

                foreach (var tile in ft.tileproperties)
                {
                    contenttexture.SetTileProperty(tile.Key + 1, tile.Value.GetTileProperty());
                }

                area.MapTextures.Add(key, contenttexture);


            }

            return area;
        }

    }
}