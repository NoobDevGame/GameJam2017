using NoobFight.Contract;
using NoobFight.Contract.Simulation;

namespace NoobFight.Core.Simulation.Components
{
    public class GravitySimulationComponent : SimulationComponents
    {
        public const float FallAcceleration = 9.81f;

        public override void SimulateWorld(IWorld world, GameTime gameTime)
        {
            foreach (var area in world.CurrentMap.Areas)
            {
                foreach (var entity in area.Entities)
                {
                    

                }
            }
        }
    }
}