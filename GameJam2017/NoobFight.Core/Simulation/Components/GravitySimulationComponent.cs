using NoobFight.Contract;
using NoobFight.Contract.Simulation;

namespace NoobFight.Core.Simulation.Components
{
    public class GravitySimulationComponent : SimulationComponent
    {
        public const float FallAcceleration = 9.81f;

        public override void SimulateWorld(IWorld world, GameTime gameTime)
        {
            var velocitychange = new Vector2(0,(float)(FallAcceleration * gameTime.ElapsedTime.TotalSeconds));

            foreach (var area in world.CurrentMap.Areas)
            {
                foreach (var entity in area.Entities)
                {
                    Vector2 velocity = entity.Velocity + velocitychange;
                }
            }
        }
    }
}