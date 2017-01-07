using NoobFight.Contract;
using NoobFight.Contract.Simulation;

namespace NoobFight.Core.Simulation.Components
{
    public abstract class SimulationComponent
    {
        public abstract void SimulateWorld(World world,GameTime gameTime);
    }
}