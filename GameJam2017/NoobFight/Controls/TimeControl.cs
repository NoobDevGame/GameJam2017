using MonoGameUi;
using NoobFight.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using engenious;

namespace NoobFight.Controls
{
    internal class TimeControl : Label
    {
        public TimeSpan Time { get; set; }

        public TimeControl(ScreenComponent manager, string style = "") : base(manager, style)
        {

        }

        protected override void OnUpdate(GameTime gameTime)
        {
            base.OnUpdate(gameTime);
            if (Time != null)
                Text = string.Format("{0:d2}:{1:d2}:{2:d2}", Time.Hours, Time.Minutes, Time.Seconds);
            else
                Text = "--:--:--";
        }
    }
}
