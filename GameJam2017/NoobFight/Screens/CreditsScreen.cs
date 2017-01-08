using MonoGameUi;
using NoobFight.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;

namespace NoobFight.Screens
{
    public class CreditsScreen : Screen
    {
        StackPanel stack;

        ScrollContainer scroll;

        Label measureLabel;

        ScreenComponent manager;

        public CreditsScreen(ScreenComponent manager) : base(manager)
        {
            this.manager = manager;

            Padding = new Border(0, 0, 0, 0);

            var lines = File.ReadLines(@"Content\credits.txt");

            measureLabel = new Label(manager);
            measureLabel.Text = "";
            Controls.Add(measureLabel);

            scroll = new ScrollContainer(manager);
            scroll.VerticalAlignment = VerticalAlignment.Stretch;
            scroll.HorizontalAlignment = HorizontalAlignment.Stretch;
            Controls.Add(scroll);
            scroll.VerticalScrollbarVisible = false;

            stack = new StackPanel(manager);
            stack.Margin = new Border(0, manager.GraphicsDevice.Viewport.Height, 0, 0);
            stack.HorizontalAlignment = HorizontalAlignment.Stretch;
            stack.Padding = new Border(0, 0, 0, 0);
            scroll.Content = stack;

            Panel spacerTop = new Panel(manager);
            spacerTop.Height = 50;
            stack.Controls.Add(spacerTop);

            foreach (var line in lines)
            {
                Label l = new Label(manager);
                l.Text = line;

                if (line == "THANKS FOR PLAYING!")
                {
                    l.TextColor = Color.Red;
                    l.Font = manager.Content.Load<SpriteFont>("HeadlineFont");
                }

                stack.Controls.Add(l);
            }

            Panel spacerBottom = new Panel(manager);
            spacerBottom.Height = manager.GraphicsDevice.Viewport.Height;
            stack.Controls.Add(spacerBottom);
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            base.OnUpdate(gameTime);
            scroll.VerticalScrollPosition += 1;
            if (scroll.VirtualSize.Y == scroll.VerticalScrollPosition + manager.GraphicsDevice.Viewport.Height)
                manager.NavigateBack();
            //stack.Margin = new Border(0, stack.Margin.Top - 3, 0, 0);

            //if(measureLabel.ActualSize.Y != 0)
            //{
            //    if (stack.Margin.Top * - 1 > stack.Controls.Sum((x) => x.ActualSize.Y + x.Margin.Top + x.Margin.Bottom)) 
            //        manager.NavigateBack();
            //}

        }
    }
}
