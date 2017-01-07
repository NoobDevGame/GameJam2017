using engenious;
using NoobFight.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight
{
    public class NoobFight : Game
    {
        public ScreenComponent ScreenManager { get; private set; }
        public NetworkComponent NetworkComponent { get; private set; }
        public SimulationComponent SimulationComponent { get; private set; }

        public NoobFight()
        {
            Content.RootDirectory = "Content";

            ScreenManager = new ScreenComponent(this);
            ScreenManager.UpdateOrder = 1;
            ScreenManager.DrawOrder = 1;

            NetworkComponent = new NetworkComponent(this);

            SimulationComponent = new SimulationComponent(this);
            SimulationComponent.UpdateOrder = 2;

            Components.Add(ScreenManager);
            Components.Add(NetworkComponent);
            Components.Add(SimulationComponent);
        }
    }
}
