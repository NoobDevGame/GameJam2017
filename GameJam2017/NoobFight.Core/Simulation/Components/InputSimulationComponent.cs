using NoobFight.Contract;
using NoobFight.Contract.Entities;
using NoobFight.Contract.Simulation;

namespace NoobFight.Core.Simulation.Components
{
    public class InputSimulationComponent : SimulationComponent
    {
        public override void SimulateWorld(IWorld world, GameTime gameTime)
        {
            foreach (var area in world.CurrentMap.Areas)
            {
                foreach (var entity in area.Entities)
                {
                    if (entity is ICharacter)
                    {
                        var character = (ICharacter) entity;
                    }
                }
            }
        }
    }
}