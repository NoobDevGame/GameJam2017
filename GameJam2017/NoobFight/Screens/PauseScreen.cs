using engenious;
using MonoGameUi;
using NoobFight.Components;
using NoobFight.Contract.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Screens
{
    public class PauseScreen : Screen
    {
        public ScreenComponent Manager;

        public PauseScreen(ScreenComponent manager) : base(manager)
        {

            Manager = manager;

            this.IsOverlay = true;

            Background = new BorderBrush(Color.Black * 0.5f);

            StackPanel stack = new StackPanel(manager);
            Controls.Add(stack);

            Button resumeButton = Button.TextButton(manager, "Resume");
            resumeButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            resumeButton.Margin = new Border(0, 0, 0, 10);
            resumeButton.MinWidth = 300;
            resumeButton.LeftMouseClick += (s, e) =>
            {
                manager.NavigateBack();
            };
            stack.Controls.Add(resumeButton);

            Button switchWorldButton = Button.TextButton(manager, "Switch World");
            switchWorldButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            switchWorldButton.Margin = new Border(0, 0, 0, 10);
            switchWorldButton.LeftMouseClick += (s, e) =>
            {
            };
            stack.Controls.Add(switchWorldButton);

            Button disconnectButton;
            if (manager.Game.SimulationComponent.Simulation.Mode == SimulationMode.Single)
            {
                manager.Game.SimulationComponent.World.Pause();

                disconnectButton = Button.TextButton(manager, "Exit");
                disconnectButton.LeftMouseClick += (s, e) =>
                {
                    manager.Game.SimulationComponent.World.Pause();
                    manager.NavigateHome();
                };
            }
            else
            {
                disconnectButton = Button.TextButton(manager, "Disconnect");
                disconnectButton.LeftMouseClick += (s, e) =>
                {
                    manager.Game.NetworkComponent.Disconnect();
                    manager.NavigateHome();
                };
            }
            disconnectButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            disconnectButton.Margin = new Border(0, 0, 0, 10);

            stack.Controls.Add(disconnectButton);

        }

        protected override void OnNavigateFrom(NavigationEventArgs args)
        {
            base.OnNavigateFrom(args);
            if (Manager.Game.SimulationComponent.Simulation.Mode == SimulationMode.Single)
            {
                Manager.Game.SimulationComponent.World.Resume();
            }
        }
    }
}
