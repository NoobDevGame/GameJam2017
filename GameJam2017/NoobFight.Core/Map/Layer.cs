using NoobFight.Contract.Map;

namespace NoobFight.Core.Map
{
    public class Layer : ILayer
    {
        public int Id { get; set; }

        public ITile[] Tiles { get; }

        internal Layer()
        {

        }

        public Layer(int id, ITile[] tiles)
        {
            this.Id = id;
            this.Tiles = tiles;
        }
    }
}