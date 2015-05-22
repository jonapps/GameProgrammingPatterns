using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.Engine.Utils;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Screens
{
    class PauseScreen : Screen
    {
        View _view;
        RectangleShape _background;
        Text _pauseText;
        Text _infoText;
        long _time;

        public PauseScreen(RenderWindow w)
            : base(w)
        {
            _window.KeyPressed += window_KeyPressed;
            _background = new RectangleShape(new Vector2f(1280, 720));
            _background.Position = new Vector2f(0, 0);
            _background.FillColor = new Color(0, 0, 0, 150);

            Vector2f size = new Vector2f(w.Size.X, w.Size.Y);
            _view = new View(size / 2f, size);

            
            _pauseText = new Text("Pause", AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_THIN));
            _pauseText.CharacterSize = 224;
            FloatRect pauseBounds = _pauseText.GetLocalBounds();
            _pauseText.Origin = new Vector2f(pauseBounds.Width / 2f + 12, pauseBounds.Height /2f + 96);
            _pauseText.Position = new Vector2f(1280 / 2f, 720 / 2f);

            _infoText = new Text("Press >SPACE< or >RETURN< to continue!", AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_LIGHT));
            _infoText.CharacterSize = 32;
            FloatRect infoBounds = _infoText.GetLocalBounds();
            _infoText.Origin = new Vector2f(infoBounds.Width, infoBounds.Height) / 2f;
            _infoText.Position = new Vector2f(1280 / 2f, 720 / 2f + infoBounds.Height / 2f + pauseBounds.Height / 2f + 10);


        }

        void window_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            if (e.Code == SFML.Window.Keyboard.Key.Return || e.Code == SFML.Window.Keyboard.Key.Space)
            {
                _screenManager.Pop();
            }
        }

        public override void Update()
        {
            _infoText.Color = new Color(255, 255, 255, (byte)(System.Math.Sin(JGerdesJWiemers.Game.Game.ElapsedTime / 200f) * 128 + 128));
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            renderTarget.SetView(_view);
            renderTarget.Draw(_background);
            renderTarget.Draw(_pauseText);
            renderTarget.Draw(_infoText);
        }

        public override void Exit()
        {
            _window.KeyPressed -= window_KeyPressed;
        }


    }
}
