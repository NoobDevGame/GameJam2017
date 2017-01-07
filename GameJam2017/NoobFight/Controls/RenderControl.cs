using MonoGameUi;
using NoobFight.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;

namespace NoobFight.Controls
{
    public class RenderControl : Control
    {
        ScreenComponent manager;

        public RenderControl(ScreenComponent manager, string style = "") : base(manager, style)
        {
            this.manager = manager;
        }

        protected override void OnDraw(SpriteBatch batch, Rectangle controlArea, GameTime gameTime)
        {
            base.OnDraw(batch, controlArea, gameTime);
            batch.DrawString(manager.Content.Load<SpriteFont>("HeadlineFont"), "Test123", new Vector2(100, 100), Color.Red);
        }
    }
}
