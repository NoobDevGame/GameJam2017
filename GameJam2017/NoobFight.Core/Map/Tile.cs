using NoobFight.Contract.Map;

namespace NoobFight.Core.Map
{
    public class Tile : ITile
    {

        public TYPE Type { get; set; }

        public Tile(int id, TileProperty property)
        {
        }
    }
}