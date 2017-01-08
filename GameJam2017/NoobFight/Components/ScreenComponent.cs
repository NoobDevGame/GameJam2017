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

        public Brush DropDownBrush { get; set; }
        public Brush ScrollBackgroundBrush { get; set; }
        public Brush ScrollForegroundBrush { get; set; }

        public ScreenComponent(NoobFight game) : base(game)
        {
            Game = game;
            TitlePrefix = "NoobFight";
        }

        public void Exit()
        {
            Game.Exit();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            Skin.Current.ButtonBrush =
               NineTileBrush.FromSingleTexture(
                   Content.Load<Texture2D>("ui/buttons/button_grey_flat"),
                       15, 15);

            Skin.Current.ButtonHoverBrush =
                NineTileBrush.FromSingleTexture(
                    Content.Load<Texture2D>("ui/buttons/button_grey_hovered"),
                        15, 15);

            Skin.Current.ButtonPressedBrush =
                NineTileBrush.FromSingleTexture(
                    Content.Load<Texture2D>("ui/buttons/button_grey_pressed"),
                    15, 15);

            Skin.Current.ProgressBarBrush =
                NineTileBrush.FromSingleTexture(
                    Content.Load<Texture2D>("ui/progress/progress_red"),
                    10, 8);

            Skin.Current.HorizontalScrollBackgroundBrush =
                NineTileBrush.FromSingleTexture(
                    Content.Load<Texture2D>("ui/progress/progress_background"),
                    10, 8);

            Skin.Current.VerticalScrollForegroundBrush = NineTileBrush.FromSingleTexture(Content.Load<Texture2D>("ui/panels/slate_panel"), 8, 8);
            Skin.Current.VerticalScrollBackgroundBrush = new BorderBrush(Color.Gray * 0.5f, LineType.Solid, Color.Black * 0.6f);

            // Control
            Skin.Current.ControlSkins[typeof(Button)] += (c) =>
            {
                Button button = c as Button;
                button.Padding = new Border(5, 5, 5, 10);
            };

            Skin.Current.PanelBrush = NineTileBrush.FromSingleTexture(Content.Load<Texture2D>("ui/panels/grey_panel"), 8, 8);

            // Control
            Skin.Current.ControlSkins[typeof(Listbox<>)] += (c) =>
            {
                dynamic button = c;
                button.Background = NineTileBrush.FromSingleTexture(Content.Load<Texture2D>("ui/panels/grey_panel"), 8, 8);
                button.SelectedItemBrush = new BorderBrush(Color.Gray * 0.3f);
                button.Padding = Border.All(5); 
            };

            Skin.Current.ControlSkins[typeof(ScrollContainer)] += (c) =>
            {
                ScrollContainer s = c as ScrollContainer;
                //s.Background = NineTileBrush.FromSingleTexture(Content.Load<Texture2D>("ui/panels/grey_panel"), 8, 8);
            };

            //Combobox
            Skin.Current.ControlSkins.Add(typeof(Combobox<>), (c) =>
            {
                dynamic comboBox = c;
                comboBox.Background = NineTileBrush.FromSingleTexture(Content.Load<Texture2D>("ui/panels/grey_panel"), 8, 8);
                comboBox.Selector.Background = NineTileBrush.FromSingleTexture(Content.Load<Texture2D>("ui/other/textbox"), 8, 8);
            });

            Skin.Current.ControlSkins.Add(typeof(Textbox), (c) =>
            {
                Textbox s = c as Textbox;
                s.Background = NineTileBrush.FromSingleTexture(Content.Load<Texture2D>("ui/other/textbox"), 8, 8);
                s.Padding = Border.All(10);
              
            });

            Skin.Current.ControlSkins.Add(typeof(Image), (c) =>
            {
                Image s = c as Image;
                s.Padding = Border.All(10);

            });

            Skin.Current.SelectedItemBrush = new BorderBrush(Color.Gray * 0.3f);

            //Frame.Background = new BorderBrush(Color.CornflowerBlue);
            Frame.Background = new TextureBrush(Content.Load<Texture2D>("ui/bg"), TextureBrushMode.Stretch);


            NavigateFromTransition = new AlphaTransition(Frame, Transition.Linear, TimeSpan.FromMilliseconds(200), 0f);
            NavigateToTransition = new AlphaTransition(Frame, Transition.Linear, TimeSpan.FromMilliseconds(200), 1f);

            NavigateToScreen(new MainScreen(this));
        }

        public Grid LabelControl(Control c, string label)
        {
            Grid g = new Grid(this);
            g.Columns.Add(new ColumnDefinition() { ResizeMode = ResizeMode.Auto });
            g.Columns.Add(new ColumnDefinition() { Width = 1, ResizeMode = ResizeMode.Parts });
            g.Rows.Add(new RowDefinition() { ResizeMode = ResizeMode.Auto });

            Label l = new Label(this) { Text = label };

            g.AddControl(l, 0, 0);
            g.AddControl(c, 1, 0);
            return g;

        }

        public override void Initialize()
        {

        }
    }
}
