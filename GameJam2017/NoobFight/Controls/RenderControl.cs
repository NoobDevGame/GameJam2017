using MonoGameUi;
using NoobFight.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;
using engenious.Graphics;
using NoobFight.Core.Map;

namespace NoobFight.Controls
{
    public class RenderControl : Control
    {
        ScreenComponent manager;
        private Area area;

        private Texture2D _pixel;

        public RenderControl(ScreenComponent manager, string style = "") : base(manager, style)
        {
            this.manager = manager;
            _pixel = new Texture2D(manager.GraphicsDevice,1,1);
            _pixel.SetData(new Color[] {Color.White });

            area = MapLoader.LoadArea("test");
        }

        protected override void OnDraw(SpriteBatch batch, Rectangle controlArea, GameTime gameTime)
        {
            base.OnDraw(batch, controlArea, gameTime);
            foreach (var layer in area.Layers)
            {
                for (int x = 0; x < area.Width; x++)
                {
                    for (int y = 0; y < area.Height; y++)
                    {
                        var id = layer.Tiles[x + y * area.Width];

                        if (id == 0)
                            continue;

                        var texturemap = area.GetMapTextures(id);

                        var texture = manager.Content.Load<Texture2D>(texturemap.Key);

                        id -= texturemap.Firstgid;
                        var tx = (id % texturemap.Columns) *(texturemap.Tilewidth + texturemap.Spacing);
                        var ty = (id / texturemap.Columns) *(texturemap.Tileheight + texturemap.Spacing);

                        Rectangle source = new Rectangle(tx, ty, texturemap.Tilewidth, texturemap.Tileheight);
                        Rectangle destination = new Rectangle(x*70,y*70,70,70);
                        batch.Draw(texture,destination,source,Color.White);
                    }
                }
            }
        }
    }
}
