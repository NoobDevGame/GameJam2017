using MonoGameUi;
using NoobFight.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;

namespace NoobFight.Screens
{
    public class LobbyScreen : Screen
    {
        Label connectedPlayers;

        Button startButton, exitButton;

        Listbox<string> playerList;

        ScreenComponent Manager;

        public LobbyScreen(ScreenComponent manager) : base(manager)
        {
            Manager = manager;

            Grid grid = new Grid(manager);
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            grid.Columns.Add(new ColumnDefinition() { Width = 1, ResizeMode = ResizeMode.Parts });
            grid.Columns.Add(new ColumnDefinition() { Width = 1, ResizeMode = ResizeMode.Parts });
            grid.Columns.Add(new ColumnDefinition() {ResizeMode = ResizeMode.Auto });
            grid.Rows.Add(new RowDefinition() { Height = 1, ResizeMode = ResizeMode.Parts });
            grid.Rows.Add(new RowDefinition() { ResizeMode = ResizeMode.Auto });
            Controls.Add(grid);

            Panel objectivePanel = new Panel(manager);
            objectivePanel.VerticalAlignment = VerticalAlignment.Stretch;
            objectivePanel.HorizontalAlignment = HorizontalAlignment.Stretch;
            objectivePanel.Background = NineTileBrush.FromSingleTexture(manager.Content.Load<Texture2D>("ui/panels/grey_panel"), 8, 8);
            objectivePanel.Controls.Add(new Label(Manager) { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Text = "Hier könnten Objectives stehen..." });
            objectivePanel.Padding = Border.All(10);
            grid.AddControl(objectivePanel, 0, 0, 2, 1);

            playerList = new Listbox<string>(manager);
            playerList.VerticalAlignment = VerticalAlignment.Stretch;
            playerList.TemplateGenerator += (s) =>
            {
                Panel p = new Panel(manager);
                p.HorizontalAlignment = HorizontalAlignment.Stretch;

                p.Controls.Add(new Label(manager) { Text = s });
                return p;
            };
            playerList.MinWidth = 200;
            playerList.Margin = new Border(5, 0, 0, 5);
            foreach (var p in manager.Game.SimulationComponent.World.Players)
                playerList.Items.Add(p.Name);
            grid.AddControl(playerList, 2, 0, 1, 2);

            startButton = Button.TextButton(manager, "Start");
            startButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            startButton.Height = 40;
            startButton.LeftMouseClick += (s, e) =>
            {
                manager.Game.NetworkComponent.StartWorld();
            };
            grid.AddControl(startButton, 0, 1);

            exitButton = Button.TextButton(manager, "Exit");
            exitButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            exitButton.Height = 40;
            exitButton.LeftMouseClick += (s, e) =>
            {
                //EXIT HERE
            };
            grid.AddControl(exitButton, 1, 1);

        }

        protected override void OnUpdate(GameTime gameTime)
        {
            base.OnUpdate(gameTime);

            if(Manager.Game.SimulationComponent.World.Players.Count() != playerList.Items.Count)
            {
                playerList.Items.Clear();
                foreach (var p in Manager.Game.SimulationComponent.World.Players)
                    playerList.Items.Add(p.Name);
            }
        }
    }
}
