using engenious;
using MonoGameUi;
using NoobFight.Components;
using NoobFight.Contract.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoobFight.Core.Map;

namespace NoobFight.Screens
{
    public class SingleplayerScreen : Screen
    {
        public SingleplayerScreen(ScreenComponent manager) : base(manager)
        {
            Padding = new Border(0, 0, 0, 0);

            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;

            Grid grid = new Grid(manager);
            grid.Margin = new Border(20, 80, 20, 20);
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            Controls.Add(grid);

            grid.Columns.Add(new ColumnDefinition() { Width = 1, ResizeMode = ResizeMode.Parts });

            grid.Rows.Add(new RowDefinition() { ResizeMode = ResizeMode.Auto });
            grid.Rows.Add(new RowDefinition() { Height = 1, ResizeMode = ResizeMode.Parts });
            grid.Rows.Add(new RowDefinition() { ResizeMode = ResizeMode.Auto });

            Combobox<string> gamemodeSelect = new Combobox<string>(manager);
            gamemodeSelect.TemplateGenerator = (s) =>
            {
                return new Label(manager) { Text = s };
            };
            foreach(var item in Enum.GetValues(typeof(GameMode)))
                gamemodeSelect.Items.Add(item.ToString());
            gamemodeSelect.SelectFirst();
            gamemodeSelect.HorizontalAlignment = HorizontalAlignment.Stretch;
            gamemodeSelect.Height = 50;
            gamemodeSelect.Margin = new Border(10, 10, 10, 0);
            grid.AddControl(gamemodeSelect, 0, 0);

            Listbox<string> mapList = new Listbox<string>(manager);
            mapList.TemplateGenerator = (s) =>
            {
                Panel p = new Panel(manager);
                p.HorizontalAlignment = HorizontalAlignment.Stretch;

                p.Controls.Add(new Label(manager) { Text = s });
                return p;
            };
            mapList.Items.Add("Testmap"); //TODO: Make dynamic!
            mapList.HorizontalAlignment = HorizontalAlignment.Stretch;
            mapList.VerticalAlignment = VerticalAlignment.Stretch;
            mapList.Margin = new Border(10, 20, 10, 10);
            grid.AddControl(mapList, 0, 1);

            Button playButton = Button.TextButton(manager, "Play!");
            playButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            playButton.Margin = Border.All(10);
            playButton.LeftMouseClick += (s, e) =>
            {
                manager.Game.SimulationComponent.CreateSinglePlayerSimulation((GameMode)Enum.Parse(typeof(GameMode), (string)gamemodeSelect.SelectedItem),manager.Game.PlayerComponent.PlayerTexture,manager.Game.PlayerComponent.PlayerName);
                manager.NavigateToScreen(new GameScreen(manager));
            };
            grid.AddControl(playButton, 0, 2);

            Button backButton = Button.TextButton(manager, "Back");
            backButton.HorizontalAlignment = HorizontalAlignment.Left;
            backButton.VerticalAlignment = VerticalAlignment.Top;
            backButton.Margin = new Border(10, 10, 10, 10);
            backButton.LeftMouseClick += (s, e) => { manager.NavigateBack(); };
            Controls.Add(backButton);
        }

        
    }
}
