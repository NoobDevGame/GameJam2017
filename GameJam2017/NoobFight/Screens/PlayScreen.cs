using MonoGameUi;
using NoobFight.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Screens
{
    public class PlayScreen : Screen
    {
        public PlayScreen(ScreenComponent manager) : base(manager)
        {
            Padding = new Border(0, 0, 0, 0);

            StackPanel stack = new StackPanel(manager);
            Controls.Add(stack);

            Button singlePlayerButton = Button.TextButton(manager, "Singleplayer");
            singlePlayerButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            singlePlayerButton.Margin = new Border(0, 0, 0, 10);
            singlePlayerButton.MinWidth = 300;
            singlePlayerButton.LeftMouseClick += (s, e) =>
            {
                manager.NavigateToScreen(new SingleplayerScreen(manager));
            };
            stack.Controls.Add(singlePlayerButton);

            Button directMultiplayerButton = Button.TextButton(manager, "Direct Multiplayer");
            directMultiplayerButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            directMultiplayerButton.Margin = new Border(0, 0, 0, 10);
            directMultiplayerButton.LeftMouseClick += (s, e) =>
            {
                manager.NavigateToScreen(new DirectConnectScreen(manager));
            };
            stack.Controls.Add(directMultiplayerButton);

            Button lobbyBrowserButton = Button.TextButton(manager, "Lobby Browser");
            lobbyBrowserButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            lobbyBrowserButton.Margin = new Border(0, 0, 0, 10);
            lobbyBrowserButton.LeftMouseClick += (s, e) =>
            {
                //manager.NavigateToScreen(new LoadScreen(manager));
            };
            stack.Controls.Add(lobbyBrowserButton);

            Button createGameButton = Button.TextButton(manager, "Create Game");
            createGameButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            createGameButton.Margin = new Border(0, 0, 0, 10);
            createGameButton.LeftMouseClick += (s, e) =>
            {
                //manager.NavigateToScreen(new LoadScreen(manager));
            };
            stack.Controls.Add(createGameButton);

            Button backButton = Button.TextButton(manager, "Back");
            backButton.HorizontalAlignment = HorizontalAlignment.Left;
            backButton.VerticalAlignment = VerticalAlignment.Top;
            backButton.Margin = new Border(10, 10, 10, 10);
            backButton.LeftMouseClick += (s, e) => { manager.NavigateBack(); };
            Controls.Add(backButton);
        }
    }
}
