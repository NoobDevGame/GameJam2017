using NoobFight.Components;
using NoobFight.Contract.Map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious.Graphics;
using OpenTK.Graphics.OpenGL;

namespace NoobFight.Controls
{
    class AreaRenderer : IDisposable
    {
        private LayerRenderer[] _layerRenderer;
        public IArea Area { get; private set; }
        private ScreenComponent _screen;
        private Texture2DArray _tiles;
        public AreaRenderer(ScreenComponent screen,IArea area)
        {
            Area = area;
            _screen = screen;
            RebuildTiles();
            RebuildRenderer();
        }

        private void RebuildTiles()
        {
            if (Area.MapTextures.Count == 0)
                return;
            int tileCount = 0;
            int width = Area.MapTextures.First().Value.Tilewidth, height = Area.MapTextures.First().Value.Tileheight;
            foreach (var set in Area.MapTextures)
            {
                if (set.Value.Tilewidth != width || set.Value.Tileheight != height)
                    throw new NotSupportedException("non uniform tile sizes not supported");
                tileCount += set.Value.Tilecount;
            }
            _tiles = new Texture2DArray(_screen.GraphicsDevice, 1, width, height, tileCount);
            int tileIndex = 0;

            int[] tileBuffer = new int[width * height];
            foreach (var set in Area.MapTextures)
            {
                int x = 0, y = 0;
                var text = _screen.Content.Load<Texture2D>(set.Key);
                int[] buffer = new int[text.Width * text.Height];
                text.GetData(buffer);
                for (int i = 0; i < buffer.Length; i++)
                {
                    //buffer[i] = -1;
                }
                int curColumn=0;
                for (int currentTile=0;currentTile<set.Value.Tilecount;currentTile++)
                {

                    int yOffset = 0;
                    int curWidth = Math.Min(text.Width - x, width);
                    if (curWidth != width)
                        Array.Clear(tileBuffer,0,tileBuffer.Length);
                    for (int i = 0; i < text.Width * height; i += text.Width, yOffset++)
                    {
                        Array.Copy(buffer, (y * text.Width + x) + i, tileBuffer, yOffset * width + (width-curWidth)/2, curWidth);
                    }
                    using (var fs = new System.IO.FileStream("test.raw", System.IO.FileMode.OpenOrCreate,
                        System.IO.FileAccess.Write))
                    using (BinaryWriter writer = new BinaryWriter(fs))
                    {
                        for (int i = 0; i < tileBuffer.Length; i++)
                            writer.Write(tileBuffer[i]);
                    }
                    _tiles.SetData(tileBuffer, tileIndex++);

                    x += width + set.Value.Spacing;
                    curColumn++;
                    if (x >= text.Width || curColumn >= set.Value.Columns)
                    {
                        y += height + set.Value.Spacing;
                        x = 0;
                        curColumn = 0;
                    }
                }

            }
        }

        private void RebuildRenderer()
        {
            var layers = Area.Layers.ToArray();
            _layerRenderer = new LayerRenderer[layers.Length];
            for (int index = 0; index < layers.Length; index++)
            {
                _layerRenderer[index] = new LayerRenderer(_screen,Area.Width, Area.Height, layers[index],_tiles);
            }
        }

        public void Render(Effect effect)
        {
            effect.Parameters["TileTextures"].SetValue(_tiles);
            _tiles.SamplerState = SamplerState.LinearClamp;
            for (int index = 0; index < _layerRenderer.Length; index++)
            {
                _layerRenderer[index].Render();
            }
        }

        public void Dispose()
        {
            foreach(var layer in _layerRenderer)
            {
                layer.Dispose();
            }
            _tiles?.Dispose();
        }
    }
}
