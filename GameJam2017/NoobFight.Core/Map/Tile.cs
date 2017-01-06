using NoobFight.Contract.Map;

namespace NoobFight.Core.Map
{
    public class Tile : ITile
    {
        public int Asset { get; private set; }

        public bool Collidable { get; private set; }

        public int X { get; set; }

        public int Y { get; set; }

        public Tile(int asset, bool collidable)
        {
            this.Asset = asset;
            this.Collidable = collidable;
        }

        public Tile(int asset, bool collidable, int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Asset = asset;
            this.Collidable = collidable;
        }
    }
}