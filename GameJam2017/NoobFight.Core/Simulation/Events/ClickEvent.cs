using NoobFight.Contract.Map;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;
using NoobFight.Contract;

namespace NoobFight.Core.Simulation.Events
{
    class ClickEvent : WorldEvent
    {
        private IEntity entity;
        private IActiveTile tile;
        private Vector2 clickPosition;

        public override void Dispatch(IWorld world, ISimulation simulation)
        {
            tile.OnClick(world.CreateNewManipulator(), entity, clickPosition);
        }

        public ClickEvent(IActiveTile BlockType, IEntity entity, Vector2 clickPosition)
        {
            tile = BlockType;
            this.entity = entity;
            this.clickPosition = clickPosition;
        }
    }
}
