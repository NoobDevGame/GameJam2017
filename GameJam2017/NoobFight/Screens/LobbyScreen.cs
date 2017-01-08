using MonoGameUi;
using NoobFight.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;

namespace NoobFight.Screens
{
    public class LobbyScreen : Screen
    {
        Label connectedPlayers;

        Button startButton, exitButton;

        ScreenComponent Manager;

        public LobbyScreen(ScreenComponent manager) : base(manager)
        {
            Manager = manager;

            StackPanel mainStack = new StackPanel(manager);
            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;
            Controls.Add(mainStack);

            connectedPlayers = new Label(manager);
            connectedPlayers.Text = "Connected Players: --";
            mainStack.Controls.Add(connectedPlayers);

            startButton = Button.TextButton(manager, "Start");
            startButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            startButton.Height = 40;
            startButton.LeftMouseClick += (s, e) =>
            {
                //DO STUFF HERE
            };
            mainStack.Controls.Add(startButton);

            exitButton = Button.TextButton(manager, "Exit");
            exitButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            exitButton.Height = 40;
            exitButton.LeftMouseClick += (s, e) =>
            {
                //EXIT HERE
            };
            mainStack.Controls.Add(exitButton);

        }

        protected override void OnUpdate(GameTime gameTime)
        {
            base.OnUpdate(gameTime);

            connectedPlayers.Text = "Connected Players: " + Manager.Game.SimulationComponent.World.Players.Count();
        }
    }
}
