using engenious;
using engenious.Graphics;
using MonoGameUi;
using NoobFight.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Screens
{
    public class OptionsScreen : Screen
    {
        public class PlayerTexture
        {
            public string TextureName { get; set; }
            public Texture2D Texture { get; set; }

            public PlayerTexture(string name, Texture2D texture)
            {
                TextureName = name;
                Texture = texture;
            }
        }

        public OptionsScreen(ScreenComponent manager) : base(manager)
        {
            StackPanel mainStack = new StackPanel(manager);
            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;
            Controls.Add(mainStack);

            var playerTextures = manager.Content.ListContent("player");

            List<PlayerTexture> playerTex = new List<PlayerTexture>();

            foreach(var tex in playerTextures.Select(Path.GetFileNameWithoutExtension))
            {
                try
                {
                    var text = manager.Content.Load<Texture2D>("player/" + tex);
                    playerTex.Add(new PlayerTexture(tex, text));
                }
                catch(Exception e)
                {
                    Console.WriteLine("Texture " + tex + " not found!");
                }
            }


            Panel p = new Panel(manager);
            p.Background = NineTileBrush.FromSingleTexture(manager.Content.Load<Texture2D>("ui/panels/grey_panel"), 8, 8);
            p.Padding = Border.All(5);
            mainStack.Controls.Add(p);

            StackPanel pTexStack = new StackPanel(manager);
            p.Controls.Add(pTexStack);

            pTexStack.Controls.Add(new Label(manager) { Text = "Player: ", HorizontalAlignment = HorizontalAlignment.Left });


            Image playerImage = new Image(manager);
            playerImage.Height = 200;
            playerImage.Width = 300;
            playerImage.Texture = playerTex.FirstOrDefault().Texture;
            playerImage.Tag = 0;
            pTexStack.Controls.Add(playerImage);

            if( manager.Game.PlayerComponent.PlayerTexture != null)
            {
                if (playerTex.FirstOrDefault(t => t.TextureName == manager.Game.PlayerComponent.PlayerTexture) != null)
                {
                    var pT = playerTex.FirstOrDefault(t => t.TextureName == manager.Game.PlayerComponent.PlayerTexture);
                    playerImage.Texture = pT.Texture;
                    playerImage.Tag = playerTex.IndexOf(pT);
                }
            }

            Panel rightLeftPanel = new Panel(manager);
            rightLeftPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
            pTexStack.Controls.Add(rightLeftPanel);

            Button leftButton = Button.TextButton(manager, " < ");
            leftButton.Height = 40;
            leftButton.HorizontalAlignment = HorizontalAlignment.Left;
            leftButton.LeftMouseClick += (s, e) =>
            {
                if((int)playerImage.Tag == 0)
                {
                    playerImage.Tag = playerTex.Count - 1;
                    playerImage.Texture = playerTex.ElementAt((int)playerImage.Tag).Texture;
                }
                else
                {
                    playerImage.Tag = (int)playerImage.Tag - 1;
                    playerImage.Texture = playerTex.ElementAt((int)playerImage.Tag).Texture;
                }
            };
            rightLeftPanel.Controls.Add(leftButton);

            Button rightButton = Button.TextButton(manager, " > ");
            rightButton.Height = 40;
            rightButton.HorizontalAlignment = HorizontalAlignment.Right;
            rightButton.LeftMouseClick += (s, e) =>
            {
                if ((int)playerImage.Tag == playerTex.Count -1)
                {
                    playerImage.Tag = 0;
                    playerImage.Texture = playerTex.ElementAt((int)playerImage.Tag).Texture;
                }
                else
                {
                    playerImage.Tag = (int)playerImage.Tag + 1;
                    playerImage.Texture = playerTex.ElementAt((int)playerImage.Tag).Texture;
                }
            };
            rightLeftPanel.Controls.Add(rightButton);

            mainStack.Controls.Add(new Panel(manager) { Height = 10, Width = 10 });

            mainStack.Controls.Add(new Label(manager) { Text = "Username: ", HorizontalAlignment = HorizontalAlignment.Left });

            Textbox nameInput = new Textbox(manager);
            nameInput.HorizontalAlignment = HorizontalAlignment.Stretch;
            nameInput.Margin = new Border(0, 0, 0, 10);
            nameInput.Text = "Player";
            mainStack.Controls.Add(nameInput);

            if (manager.Game.PlayerComponent.PlayerName != null)
                nameInput.Text = manager.Game.PlayerComponent.PlayerName;

            mainStack.Controls.Add(new Panel(manager) { Height = 10, Width = 10 });

            Button saveButton = Button.TextButton(manager, "Save");
            saveButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            saveButton.LeftMouseClick += (s, e) =>
            {
                manager.Game.PlayerComponent.PlayerTexture = playerTex.ElementAt((int)playerImage.Tag).TextureName;
                manager.Game.PlayerComponent.PlayerName = nameInput.Text;
                manager.NavigateBack();
            };
            mainStack.Controls.Add(saveButton);

        }
    }
}
