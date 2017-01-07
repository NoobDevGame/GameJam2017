using engenious;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Components
{
    public class CameraComponent : GameComponent
    {
        public new NoobFight Game { get; private set; }
        public Vector2 Offset { get; private set; }
        public Vector2 HalfViewport { get; private set; }

        public CameraComponent(NoobFight game) : base(game)
        {
            Game = game;
        }
        
        public override void Update(GameTime gameTime)
        {
            var player = Game.SimulationComponent.Player;

            if (player == null)
                return;

            var viewport = new Vector2(Game.GraphicsDevice.Viewport.Width/2, Game.GraphicsDevice.Viewport.Height/2);

            Offset = viewport - new Vector2(player.Position.X * 70, player.Position.Y * 70);
            HalfViewport = viewport;

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }
    }
}
