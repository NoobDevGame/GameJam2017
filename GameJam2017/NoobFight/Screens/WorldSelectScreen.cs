using engenious;
using MonoGameUi;
using NoobFight.Components;
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
            StackPanel mainStack = new StackPanel(manager);
            Controls.Add(mainStack);

            worldList = new Listbox<string>(manager);
            worldList.HorizontalAlignment = HorizontalAlignment.Stretch;
            worldList.TemplateGenerator = (s) => new Label(manager) { Text = s , HorizontalAlignment = HorizontalAlignment.Stretch};
            worldList.SelectedItemBrush = new BorderBrush(Color.Green);
            mainStack.Controls.Add(worldList);

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
            mainStack.Controls.Add(joinButton);

            Button createButton = Button.TextButton(manager, "Create World");
            createButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            createButton.Height = 50;
            createButton.MinWidth = 300;
            createButton.LeftMouseClick += (s, e) =>
            {
               
            };
            mainStack.Controls.Add(createButton);
        }
    }
}
