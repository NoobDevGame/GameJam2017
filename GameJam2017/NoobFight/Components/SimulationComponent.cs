using engenious;
using NoobFight.Contract.Simulation;
using NoobFight.Core.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Components
{
    public class SimulationComponent : GameComponent
    {
        public new NoobFight Game { get; private set; }

        public ISimulation Simulation { get; private set; }

        public SimulationComponent(NoobFight game) : base(game)
        {
            Game = game;
            Simulation = new Simulation();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Simulation.Update(new Contract.GameTime(gameTime.ElapsedGameTime));
        }
    }
}
