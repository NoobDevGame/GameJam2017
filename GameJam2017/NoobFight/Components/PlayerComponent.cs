using engenious;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Components
{
    public class PlayerComponent : GameComponent
    {
        public new NoobFight Game { get; private set; }

        public string PlayerName { get; set; } = "Player";

        public string PlayerTexture { get; set; } = "monkey";

        public PlayerComponent(NoobFight game) : base(game)
        {
            Game = game;
        }
    }
}
