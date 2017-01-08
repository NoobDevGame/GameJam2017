using NoobFight.Contract.Map;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;
using NoobFight.Contract;
using System.IO;

namespace NoobFight.Core.Simulation.Events
{
    public class ClickEvent : WorldEvent
    {
        private IEntity entity;
        private IActiveTile tile;
        private Vector2 clickPosition;

        public override WorldEventType EventType => WorldEventType.Click;

        public override SimulationMode SimulationMode => SimulationMode.Lokal;

        public override bool ShareMode => false;

        public ClickEvent()
        {

        }
        public override void Dispatch(IWorld world, ISimulation simulation)
        {
            tile.OnClick(world.CreateNewManipulator(), entity, clickPosition);
        }

        public override byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                return ms.ToArray();
            }
        }

        public override void Deserialize(byte[] payload)
        {
        }

        public ClickEvent(IActiveTile BlockType, IEntity entity, Vector2 clickPosition)
        {
            tile = BlockType;
            this.entity = entity;
            this.clickPosition = clickPosition;
        }
    }
}
