using engenious;
using engenious.Graphics;
using MonoGameUi;
using NoobFight.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobFight.Components
{
    public class ScreenComponent : BaseScreenComponent
    {
        public new NoobFight Game { get; private set; }

        public ScreenComponent(NoobFight game) : base(game)
        {
            Game = game;
            TitlePrefix = "NoobFight";
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            Skin.Current.ButtonBrush =
               NineTileBrush.FromSingleTexture(
                   Content.Load<Texture2D>("ui/buttonLong_brown"),
                       15, 15);

            Skin.Current.ButtonHoverBrush =
                NineTileBrush.FromSingleTexture(
                    Content.Load<Texture2D>("ui/buttonLong_beige"),
                        15, 15);

            Skin.Current.ButtonPressedBrush =
                NineTileBrush.FromSingleTexture(
                    Content.Load<Texture2D>("ui/buttonLong_beige_pressed"), 
                    15, 15);

            Skin.Current.ProgressBarBrush =
                NineTileBrush.FromSingleTexture(
                    Content.Load<Texture2D>("ui/progress_red"), 
                    10, 8);

            Skin.Current.HorizontalScrollBackgroundBrush =
                NineTileBrush.FromSingleTexture(
                    Content.Load<Texture2D>("ui/progress_background"), 
                    10, 8);

            Frame.Background = new BorderBrush(Color.Brown);

            NavigateFromTransition = new AlphaTransition(Frame, Transition.Linear, TimeSpan.FromMilliseconds(200), 0f);
            NavigateToTransition = new AlphaTransition(Frame, Transition.Linear, TimeSpan.FromMilliseconds(200), 1f);

            NavigateToScreen(new MainScreen(this));
        }

        public void Exit()
        {
            Game.Exit();
        }
    }
}
