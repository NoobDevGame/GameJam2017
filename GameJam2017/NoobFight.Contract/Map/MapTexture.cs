

using NoobFight.Core.Map;

namespace NoobFight.Contract.Map
{
    public struct MapTexture
    {
        public int Tilecount;
        public string Key;
        public int Firstgid;
        public int Spacing;
        public int Tileheight;
        public int Tilewidth;

        public int Columns;

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