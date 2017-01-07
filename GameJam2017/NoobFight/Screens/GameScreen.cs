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

        public GameScreen(ScreenComponent manager) : base(manager)
        {
            Manager = manager;

            Padding = new Border(0, 0, 0, 0);

            RenderControl renderControl = new RenderControl(manager);
            renderControl.VerticalAlignment = VerticalAlignment.Stretch;
            renderControl.HorizontalAlignment = HorizontalAlignment.Stretch;
            Controls.Add(renderControl);
        }

        protected override void OnUpdate(engenious.GameTime gameTime)
        {
            base.OnUpdate(gameTime);
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
        }
    }
}
