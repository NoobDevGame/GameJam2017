

namespace NoobFight.Core.Map
{
    public class MapTexture
    {
        public int Tilecount { get; }
        public string Key { get; }
        public int Firstgid { get; }
        public int Spacing { get; }
        public int Tileheight { get; }
        public int Tilewidth { get; }

        public int Columns { get; }

        private TileProperty[] _properties;

        public MapTexture(string key, int firstgid, int tilecount, int spacing, int tileheight, int tilewidth,int columns)
        {
            Tilecount = tilecount;
            Key = key;
            Firstgid = firstgid;
            Spacing = spacing;
            Tileheight = tileheight;
            Tilewidth = tilewidth;
            Columns = columns;


            _properties = new TileProperty[tilecount];
        }

        public void SetTileProperty(int id, TileProperty tileProperty)
        {
            _properties[id] = tileProperty;
        }

    }
}