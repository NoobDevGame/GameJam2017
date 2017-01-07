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

namespace NoobFight.Screens
{
    public class GameScreen : Screen
    {
        ScreenComponent Manager;

        HealthBarControl HealthBar;
        TimeControl TimeControl;

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

            TimeControl = new TimeControl(manager);
            TimeControl.VerticalAlignment = VerticalAlignment.Top;
            TimeControl.HorizontalAlignment = HorizontalAlignment.Left;
            Controls.Add(TimeControl);
        }

        protected override void OnUpdate(engenious.GameTime gameTime)
        {
            base.OnUpdate(gameTime);

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

            var mouse = Mouse.GetState();

            if (mouse.IsButtonDown(MouseButton.Left))
                input.LeftClick = true;
            
            if (mouse.IsButtonDown(MouseButton.Right))
                input.RightClick = true;

            Manager.Game.SimulationComponent.Player.Input = input;

            HealthBar.Value = Manager.Game.SimulationComponent.Player.Health;
        }
    }
}
