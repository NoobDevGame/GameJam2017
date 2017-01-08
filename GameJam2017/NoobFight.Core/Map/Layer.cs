using NoobFight.Contract.Map;

namespace NoobFight.Core.Map
{
    public class Layer : ILayer
    {
        public int Id { get; set; }
        public int[] Tiles { get; }

        public Layer(int id, int[] tiles)
        {
            this.Id = id;
            this.Tiles = tiles;
        }
    }
}