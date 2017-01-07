using NoobFight.Components;
using NoobFight.Contract.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Controls
{
    class AreaRenderer
    {
        private LayerRenderer[] _layerRenderer;
        private IArea _area;
        private ScreenComponent _screen;
        public AreaRenderer(ScreenComponent screen,IArea area)
        {
            _area = area;
            _screen = screen;
            RebuildRenderer();
        }
        private void RebuildRenderer()
        {
            var layers = _area.Layers.ToArray();
            _layerRenderer = new LayerRenderer[layers.Length];
            for (int index = 0; index < layers.Length; index++)
            {
                //_layerRenderer[index] = new LayerRenderer(_area.Width, _area.Height, layers[index]);
            }
        }
    }
}
