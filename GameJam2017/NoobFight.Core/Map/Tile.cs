using System;
using NoobFight.Contract.Map;

namespace NoobFight.Core.Map
{
    public class Tile : ITile
    {
        public int Asset { get; private set; }

        public bool Collidable { get; private set; }

        public Tile(int Asset, bool Collidable)
        {
            this.Asset = Asset;
            this.Collidable = Collidable;
        }
    }
}