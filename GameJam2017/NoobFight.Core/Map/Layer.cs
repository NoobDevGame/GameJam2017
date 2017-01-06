using NoobFight.Contract.Map;

namespace NoobFight.Core.Map
{
    public class Layer : ILayer
    {
        public ITile[,] Tiles { get; }

        public Layer(int width, int height)
        {
            Tiles = new ITile[width,height];
        }
    }
}