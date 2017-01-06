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

        public NoobFight()
        {
            Content.RootDirectory = "Content";

            ScreenManager = new ScreenComponent(this);
            ScreenManager.UpdateOrder = 1;
            ScreenManager.DrawOrder = 1;

            NetworkComponent = new NetworkComponent(this);

            Components.Add(ScreenManager);
            Components.Add(NetworkComponent);
        }
    }
}
