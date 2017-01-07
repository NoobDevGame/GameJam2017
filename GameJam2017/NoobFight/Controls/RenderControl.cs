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

        private Texture2D playerTexture;

        public RenderControl(ScreenComponent manager, string style = "") : base(manager, style)
        {
            this.manager = manager;
            _pixel = new Texture2D(manager.GraphicsDevice, 1, 1);

            playerTexture = manager.Content.Load<Texture2D>("monkey");
        }

        protected override void OnDraw(SpriteBatch batch, Rectangle controlArea, GameTime gameTime)
        {
            base.OnDraw(batch, controlArea, gameTime);

            var player = manager.Game.SimulationComponent.Player;
            var area = player.CurrentArea;

            if (area == null)
                return;


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
                        Rectangle destination = new Rectangle(x * 70, y * 70, 70, 70);
                        batch.Draw(texture, destination, source, Color.White);
                    }
                }
            }

            foreach (var entity in area.Entities)
            {
                Rectangle destination = new Rectangle((int)((entity.Position.X - entity.Radius) * 70), (int)((entity.Position.Y - entity.Height) * 70), (int)(entity.Radius * 2 * 70), (int)(entity.Height * 70));
                batch.Draw(playerTexture, destination, Color.White);
            }

        }
    }
}
