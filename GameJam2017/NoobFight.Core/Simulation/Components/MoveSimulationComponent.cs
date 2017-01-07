using NoobFight.Contract;
using NoobFight.Contract.Simulation;

namespace NoobFight.Core.Simulation.Components
{
    public class MoveSimulationComponent : SimulationComponent
    {
        public override void SimulateWorld(IWorld world, GameTime gameTime)
        {
            foreach (var area in world.CurrentMap.Areas)
            {
                foreach (var entity in area.Entities)
                {
                    entity.Move = entity.Velocity * gameTime.ElapsedTime.TotalSeconds;
                }
            }
        }
    }
}