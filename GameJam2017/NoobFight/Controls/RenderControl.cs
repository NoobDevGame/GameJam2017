﻿using MonoGameUi;
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

        private Texture2D _pixel;
        

        public RenderControl(ScreenComponent manager, string style = "") : base(manager, style)
        {
            this.manager = manager;
            _pixel = new Texture2D(manager.GraphicsDevice, 1, 1);
            
        }

        protected override void OnDraw(SpriteBatch batch, Rectangle controlArea, GameTime gameTime)
        {
            base.OnDraw(batch, controlArea, gameTime);



            var player = manager.Game.SimulationComponent.Player;
            var area = player.CurrentArea;

            if (area == null)
                return;

            var cOffset = manager.Game.CameraComponent.Offset;

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
                        var tx = (id % texturemap.Columns) * (texturemap.Tilewidth + texturemap.Spacing);
                        var ty = (id / texturemap.Columns) * (texturemap.Tileheight + texturemap.Spacing);

                        Rectangle source = new Rectangle(tx, ty, texturemap.Tilewidth, texturemap.Tileheight);
                        Rectangle destination = new Rectangle((int)((x + player.Radius) * 70 + cOffset.X ), (int)((y+player.Height) * 70 +cOffset.Y), 70, 70);
                        batch.Draw(texture, destination, source, Color.White);
                    }
                }
            }

            foreach (var entity in area.Entities)
            {
                Vector2 position = new Vector2((entity.Position.X - entity.Radius) * 70, (entity.Position.Y - entity.Height) * 70 );

                Vector2 offset = Vector2.Zero;
                if (entity == manager.Game.SimulationComponent.Player)
                {
                    position = manager.Game.CameraComponent.HalfViewport - new Vector2(entity.Radius, entity.Height);
                }

                Rectangle destination = new Rectangle((int)(position.X), (int)(position.Y), (int)(entity.Radius * 2 * 70), (int)(entity.Height * 70));
                batch.Draw(manager.Content.Load<Texture2D>(entity.TextureName), destination, Color.White);
            }

        }
    }
}
