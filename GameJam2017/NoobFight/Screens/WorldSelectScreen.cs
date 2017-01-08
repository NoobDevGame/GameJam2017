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
    public class WorldSelectScreen : Screen
    {
        Listbox<string> worldList;

        public WorldSelectScreen(ScreenComponent manager, string[] worlds) : base(manager)
        {
            Grid grid = new Grid(manager);
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            grid.Columns.Add(new ColumnDefinition() { Width = 1, ResizeMode = ResizeMode.Parts });
            grid.Columns.Add(new ColumnDefinition() { Width = 1, ResizeMode = ResizeMode.Parts });
            grid.Rows.Add(new RowDefinition() { Height = 1, ResizeMode = ResizeMode.Parts });
            grid.Rows.Add(new RowDefinition() { ResizeMode = ResizeMode.Auto });
            Controls.Add(grid);

            worldList = new Listbox<string>(manager);
            worldList.HorizontalAlignment = HorizontalAlignment.Stretch;
            worldList.VerticalAlignment = VerticalAlignment.Stretch;
            worldList.TemplateGenerator = (s) =>
            {
                Panel p = new Panel(manager);
                p.HorizontalAlignment = HorizontalAlignment.Stretch;

                p.Controls.Add(new Label(manager) { Text = s });
                return p;
            };
            grid.AddControl(worldList, 0, 0, 2 ,1);

            foreach(var world in worlds)
            {
                worldList.Items.Add(world);

            }

            worldList.SelectFirst();

            Button joinButton = Button.TextButton(manager, "Join");
            joinButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            joinButton.Height = 50;
            joinButton.MinWidth = 300;
            joinButton.LeftMouseClick += (s, e) =>
            {
                manager.Game.NetworkComponent.JoinWorld(worldList.SelectedItem);
            };
            grid.AddControl(joinButton, 0, 1);

            Button createButton = Button.TextButton(manager, "Create World");
            createButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            createButton.Height = 50;
            createButton.MinWidth = 300;
            createButton.LeftMouseClick += (s, e) =>
            {
                manager.NavigateToScreen(new NewWorldScreen(manager));
            };
            grid.AddControl(createButton, 1, 1);
        }
    }
}
