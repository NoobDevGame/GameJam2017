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
        private IActiveTile activeTile;
        private IPlayer player;

        public override WorldEventType EventType => WorldEventType.Click;

        public override SimulationMode SimulationMode => SimulationMode.Lokal;

        public override bool ShareMode => false;

        public ClickEvent()
        {

        }
        public ClickEvent(IPlayer player, IEntity entity)
        {
            this.player = player;
            this.entity = entity;
        }

        public ClickEvent(IPlayer player, IActiveTile activeTile)
        {
            this.player = player;
            this.activeTile = activeTile;
        }

        public override void Dispatch(IWorld world, ISimulation simulation)
        {
            if (this.activeTile != null)
                activeTile.OnClick(world.CreateNewManipulator(), player);

            if (this.entity != null)
                entity.OnClick(world.CreateNewManipulator(), player);
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

    }
}
