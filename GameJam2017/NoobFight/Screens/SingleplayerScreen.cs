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
            grid.Rows.Add(new RowDefinition() { Height = 1, ResizeMode = ResizeMode.Auto });
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
            gamemodeSelect.Height = 30;
            gamemodeSelect.Background = new BorderBrush(Color.White);
            grid.AddControl(gamemodeSelect, 0, 0);

            Listbox<string> mapList = new Listbox<string>(manager);
            mapList.TemplateGenerator = (s) =>
            {
                return new Label(manager) { Text = s };
            };
            mapList.Items.Add("Testmap"); //TODO: Make dynamic!
            mapList.HorizontalAlignment = HorizontalAlignment.Stretch;
            mapList.VerticalAlignment = VerticalAlignment.Stretch;
            mapList.Background = new BorderBrush(Color.White);
            mapList.Margin = new Border(0, 10, 0, 10);
            mapList.SelectedItemBrush = new BorderBrush(Color.DarkGreen);
            grid.AddControl(mapList, 0, 1);

            Button playButton = Button.TextButton(manager, "Play!");
            playButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            playButton.Height = 50;
            playButton.LeftMouseClick += (s, e) =>
            {
                manager.Game.SimulationComponent.CreateSinglePlayerSimulation((GameMode)Enum.Parse(typeof(GameMode), (string)gamemodeSelect.SelectedItem),"pig","Hallo");
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
