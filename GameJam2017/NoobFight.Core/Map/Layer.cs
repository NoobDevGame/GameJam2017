using NoobFight.Contract.Map;

namespace NoobFight.Core.Map
{
    class Layer : ILayer
    {
        public ITile[,] Tiles { get; private set; }

        public Layer(ITile[,] Tiles)
        {
            this.Tiles = Tiles;
        }
    }
}
