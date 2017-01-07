using MonoGameUi;
using NoobFight.Components;
using NoobFight.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Screens
{
    public class GameScreen : Screen
    {
        public GameScreen(ScreenComponent manager) : base(manager)
        {
            Padding = new Border(0, 0, 0, 0);

            RenderControl renderControl = new RenderControl(manager);
            renderControl.VerticalAlignment = VerticalAlignment.Stretch;
            renderControl.HorizontalAlignment = HorizontalAlignment.Stretch;
            Controls.Add(renderControl);
        }
    }
}
