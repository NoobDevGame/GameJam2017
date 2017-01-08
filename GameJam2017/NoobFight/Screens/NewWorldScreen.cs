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
    public class NewWorldScreen : Screen
    {
        ScreenComponent Manager;

        public NewWorldScreen(ScreenComponent manager) : base(manager)
        {
            Manager = manager;

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
            grid.Rows.Add(new RowDefinition() { ResizeMode = ResizeMode.Auto });
            grid.Rows.Add(new RowDefinition() { Height = 1, ResizeMode = ResizeMode.Parts });
            grid.Rows.Add(new RowDefinition() { ResizeMode = ResizeMode.Auto });

            Combobox<string> gamemodeSelect = new Combobox<string>(manager);
            gamemodeSelect.TemplateGenerator = (s) =>
            {
                return new Label(manager) { Text = s };
            };
            foreach (var item in Enum.GetValues(typeof(GameMode)))
                gamemodeSelect.Items.Add(item.ToString());
            gamemodeSelect.SelectFirst();
            gamemodeSelect.HorizontalAlignment = HorizontalAlignment.Stretch;
            gamemodeSelect.Height = 50;
            gamemodeSelect.Margin = new Border(10, 10, 10, 0);

            var gameModeG = manager.LabelControl(gamemodeSelect, "Gamemode:");
            gameModeG.Margin = new Border(10, 0, 0, 0);

            grid.AddControl(gameModeG, 0, 0);

            Textbox nameInput = new Textbox(manager);
            nameInput.HorizontalAlignment = HorizontalAlignment.Stretch;
            nameInput.Text = "Default";
            nameInput.Margin = new Border(0, 0, 10, 0);

            var nameInputG = manager.LabelControl(nameInput, "Name: ");
            nameInputG.Margin = new Border(10, 20, 0, 0);

            grid.AddControl(nameInputG, 0, 1);

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
            grid.AddControl(mapList, 0, 2);

            Button playButton = Button.TextButton(manager, "Create");
            playButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            playButton.Margin = Border.All(10);
            playButton.LeftMouseClick += (s, e) =>
            {
                manager.Game.NetworkComponent.CreateWorld(nameInput.Text, (GameMode)Enum.Parse(typeof(GameMode), gamemodeSelect.SelectedItem));
            };
            grid.AddControl(playButton, 0, 3);

            Button backButton = Button.TextButton(manager, "Back");
            backButton.HorizontalAlignment = HorizontalAlignment.Left;
            backButton.VerticalAlignment = VerticalAlignment.Top;
            backButton.Margin = new Border(10, 10, 10, 10);
            backButton.LeftMouseClick += (s, e) => { manager.NavigateBack(); };
            Controls.Add(backButton);

        }
    }
}
