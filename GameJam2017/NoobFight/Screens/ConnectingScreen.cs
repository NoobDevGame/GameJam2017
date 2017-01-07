using MonoGameUi;
using NoobFight.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Screens
{
    public class ConnectingScreen : Screen
    {
        Label infoLabel;

        ScreenComponent Manager;

        public ConnectingScreen(ScreenComponent manager, bool error = false) : base(manager)
        {
            Manager = manager;

            infoLabel = new Label(manager);
            infoLabel.Text = "Connecting...";
            Controls.Add(infoLabel);

            if (error)
                ConnectionError();
            else
                Connect();
        }

        private void Connect()
        {
            //Connection here
        }

        private void ConnectionError()
        {
            infoLabel.Text = "Connection Error!";

            Button backButton = Button.TextButton(Manager, "Back");
            backButton.HorizontalAlignment = HorizontalAlignment.Left;
            backButton.VerticalAlignment = VerticalAlignment.Top;
            backButton.Margin = new Border(10, 10, 10, 10);
            backButton.LeftMouseClick += (s, e) => { Manager.NavigateBack(); };
            Controls.Add(backButton);
        }


    }
}
