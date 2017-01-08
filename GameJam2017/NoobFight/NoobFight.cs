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
        public CameraComponent CameraComponent { get; private set; }
        public PlayerComponent PlayerComponent { get; private set; }

        public NoobFight()
        {            
            Content.RootDirectory = "Content";

            CameraComponent = new CameraComponent(this);
            CameraComponent.UpdateOrder = 1;

            ScreenManager = new ScreenComponent(this);
            ScreenManager.UpdateOrder = 2;
            ScreenManager.DrawOrder = 1;

            NetworkComponent = new NetworkComponent(this);

            SimulationComponent = new SimulationComponent(this);
            SimulationComponent.UpdateOrder = 3;

            PlayerComponent = new PlayerComponent(this);
            Components.Add(PlayerComponent);

            Components.Add(CameraComponent);
            Components.Add(ScreenManager);
            Components.Add(NetworkComponent);
            Components.Add(SimulationComponent);

            Window.AllowUserResizing = false;
        }
    }
}
