using NoobFight.Contract.Map;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;

namespace NoobFight.Core.Simulation.Events
{
    class ClickEvent : WorldEvent
    {
        private IPlayer player;
        private IEntity entity;
        private IActiveTile activeTile;

        public override void Dispatch(IWorld world, ISimulation simulation)
        {
            if (this.activeTile != null)
                activeTile.OnClick(world.CreateNewManipulator(), player);

            if (this.entity != null)
                entity.OnClick(world.CreateNewManipulator(), player);
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
    }
}
