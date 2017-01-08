using MonoGameUi;
using NoobFight.Components;
using NoobFight.Contract;
using NoobFight.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Input;
using engenious.Graphics;

namespace NoobFight.Screens
{
    public class GameScreen : Screen
    {
        new ScreenComponent Manager;

        HealthBarControl HealthBar;
        TimeControl TimeControl;

        Label ScoreLabel;

        TimeSpan TimeOffset;

        public GameScreen(ScreenComponent manager) : base(manager)
        {
            Manager = manager;

            Padding = new Border(0, 0, 0, 0);

            RenderControl renderControl = new RenderControl(manager);
            renderControl.VerticalAlignment = VerticalAlignment.Stretch;
            renderControl.HorizontalAlignment = HorizontalAlignment.Stretch;
            Controls.Add(renderControl);

            HealthBar = new HealthBarControl(manager);
            HealthBar.VerticalAlignment = VerticalAlignment.Top;
            HealthBar.Width = 500;
            HealthBar.Maximum = 100;
            HealthBar.Value = 100;
            HealthBar.Height = 30;
            HealthBar.Margin = new Border(0, 10, 0, 0);
            Controls.Add(HealthBar);

            Panel timePanel = new Panel(manager);
            timePanel.HorizontalAlignment = HorizontalAlignment.Left;
            timePanel.VerticalAlignment = VerticalAlignment.Top;
            timePanel.Background = NineTileBrush.FromSingleTexture(manager.Content.Load<Texture2D>("ui/panels/green_panel"), 8, 8);
            timePanel.Padding = Border.All(5);
            timePanel.Margin = Border.All(5);
            Controls.Add(timePanel);

            Panel scorePanel = new Panel(manager);
            scorePanel.HorizontalAlignment = HorizontalAlignment.Right;
            scorePanel.VerticalAlignment = VerticalAlignment.Top;
            scorePanel.Background = NineTileBrush.FromSingleTexture(manager.Content.Load<Texture2D>("ui/panels/green_panel"), 8, 8);
            scorePanel.Padding = Border.All(5);
            scorePanel.Margin = Border.All(5);
            Controls.Add(scorePanel);

            ScoreLabel = new Label(manager);
            ScoreLabel.Text = "-1";
            scorePanel.Controls.Add(ScoreLabel);

            TimeControl = new TimeControl(manager);
            TimeControl.VerticalAlignment = VerticalAlignment.Top;
            TimeControl.HorizontalAlignment = HorizontalAlignment.Left;
            timePanel.Controls.Add(TimeControl);
        }

        protected override void OnKeyDown(KeyEventArgs args)
        {
            base.OnKeyDown(args);
            if (args.Key == Keys.Tab)
                Manager.NavigateToScreen(new TabScreen(Manager));
        }

        protected override void OnKeyUp(KeyEventArgs args)
        {
            base.OnKeyUp(args);

            if (Manager.ActiveScreen is TabScreen)
                Manager.NavigateBack();
        }

        protected override void OnUpdate(engenious.GameTime gameTime)
        {
            base.OnUpdate(gameTime);

            if (Manager.ActiveScreen is PauseScreen || Manager.ActiveScreen is TabScreen)
                return;

            if (TimeOffset.Milliseconds == 0)
                TimeOffset = gameTime.TotalGameTime;

            TimeControl.Time = gameTime.TotalGameTime - TimeOffset;

            var key = Keyboard.GetState();

            Input input = new Input();

            if (key.IsKeyDown(Keys.A))
                input.MoveLeft = true;

            if (key.IsKeyDown(Keys.W))
                input.Jump = true;

            if (key.IsKeyDown(Keys.D))
                input.MoveRight = true;

            if (key.IsKeyDown(Keys.Space))
                input.Jump = true;

            if (key.IsKeyDown(Keys.Escape))
                Manager.NavigateToScreen(new PauseScreen(Manager));

            var mouse = Mouse.GetState();
            if (mouse.X < Manager.Game.Window.ClientRectangle.Width && mouse.Y < Manager.Game.Window.ClientRectangle.Height)
            {
                if (mouse.IsButtonDown(MouseButton.Left))
                    input.LeftClick = true;

                if (mouse.IsButtonDown(MouseButton.Right))
                    input.RightClick = true;
                
                input.MousePosition = new Contract.Vector2(mouse.X - Manager.Game.Window.ClientRectangle.Width/2, mouse.Y- Manager.Game.Window.ClientRectangle.Height/2);
            }


            Manager.Game.SimulationComponent.Player.Input = input;

            HealthBar.Value = Manager.Game.SimulationComponent.Player.Health;

            if (int.Parse(ScoreLabel.Text) != Manager.Game.SimulationComponent.Player.Score)
                ScoreLabel.Text = Manager.Game.SimulationComponent.Player.Score.ToString();
        }
    }
}
