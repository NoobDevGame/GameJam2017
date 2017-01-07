using engenious;
using MonoGameUi;
using NoobFight.Components;
using NoobFight.Core.Network.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Screens
{
    public class DirectConnectScreen : Screen
    {
        public DirectConnectScreen(ScreenComponent manager) : base(manager)
        {
            Padding = new Border(0, 0, 0, 0);

            StackPanel stack = new StackPanel(manager);
            Controls.Add(stack);

            stack.Controls.Add(new Label(manager) { Text = "Username: ", HorizontalAlignment = HorizontalAlignment.Left });

            Textbox nameInput = new Textbox(manager);
            nameInput.HorizontalAlignment = HorizontalAlignment.Stretch;
            nameInput.Margin = new Border(0, 0, 0, 10);
            nameInput.Background = new BorderBrush(Color.White);
            stack.Controls.Add(nameInput);

            stack.Controls.Add(new Panel(manager) { Height = 10, Width = 10 });

            stack.Controls.Add(new Label(manager) { Text = "Server Address: ", HorizontalAlignment = HorizontalAlignment.Left });

            Textbox ipInput = new Textbox(manager);
            ipInput.HorizontalAlignment = HorizontalAlignment.Stretch;
            ipInput.Margin = new Border(0, 0, 0, 10);
            ipInput.Background = new BorderBrush(Color.White);
            stack.Controls.Add(ipInput);

            stack.Controls.Add(new Panel(manager) { Height = 10, Width = 10 });

            Button connectButton = Button.TextButton(manager, "Connect");
            connectButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            connectButton.Margin = new Border(0, 0, 0, 10);
            connectButton.MinWidth = 300;
            connectButton.LeftMouseClick += (s, e) =>
            {
                try
                {
                    //TODO:Nickname ändern
                    manager.Game.NetworkComponent.Connect(ipInput.Text, 667,nameInput.Text,"monkey");
                    manager.NavigateToScreen(new ConnectingScreen(manager));
                }
                catch(Exception ex)
                {
                    manager.NavigateToScreen(new ConnectingScreen(manager, true));
                }
            };
            stack.Controls.Add(connectButton);

            Button backButton = Button.TextButton(manager, "Back");
            backButton.HorizontalAlignment = HorizontalAlignment.Left;
            backButton.VerticalAlignment = VerticalAlignment.Top;
            backButton.Margin = new Border(10, 10, 10, 10);
            backButton.LeftMouseClick += (s, e) => { manager.NavigateBack(); };
            Controls.Add(backButton);
        }
    }
}
