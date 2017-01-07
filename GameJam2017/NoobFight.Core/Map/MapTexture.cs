namespace NoobFight.Core.Map
{
    public class MapTexture
    {
        private readonly string _key;
        private readonly int _firstgid;
        private readonly int _spacing;
        private readonly int _tileheight;
        private readonly int _tilewidth;

        private TileProperty[] _properties;

        public MapTexture(string key, int firstgid, int tilecount, int spacing, int tileheight, int tilewidth)
        {
            _key = key;
            _firstgid = firstgid;
            _spacing = spacing;
            _tileheight = tileheight;
            _tilewidth = tilewidth;

            _properties = new TileProperty[tilecount];
        }

        public void SetTileProperty(int id, TileProperty tileProperty)
        {
            id -= _firstgid;
            _properties[id] = tileProperty;
        }
    }
}