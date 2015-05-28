using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.Engine.Input;
using JGerdesJWiemers.Game.Engine.Utils;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMath = System.Math;

namespace JGerdesJWiemers.Game.ShootEmUp.Screens
{
    class TitleScreen : Screen
    {

        private View _view;

        private Sprite _earth;
        private Sprite _moon;
        private bool _moonFront;
        private Text _start;
        private RectangleShape _rect;

        private Clock _clock;

        public TitleScreen(RenderWindow w)
        :base(w){

            _earth = new Sprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_EARTH_FULL).Texture);
            _earth.Origin = new Vector2f(720 / 2f, 720 / 2f);
            _earth.Position = new Vector2f(1280 / 2f, 720 / 2f);
            _moon = new Sprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_MOON_FULL).Texture);
            _moon.Origin = new Vector2f(160 / 2f, 160 / 2f);

            Vector2f size = new Vector2f(w.Size.X, w.Size.Y);
            _view = new View(size / 2f, size);

            _input.On("return", delegate(InputEvent e, int c)
            {
                _screenManager.Switch(new Game(_window));
                return true;
            });

            _moonFront = false;

            _start = new Text(">press start<", AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_LIGHT));
            _start.CharacterSize = 48;
            FloatRect bounds = _start.GetLocalBounds();
            _start.Origin = new Vector2f(bounds.Width, bounds.Height) / 2f;
            _start.Position = new Vector2f(1280 / 2f, 480);

            _rect = new RectangleShape(new Vector2f(1280, 64));
            _rect.Origin = new Vector2f(1280 / 2f, 64 / 2f);
            _rect.Position = new Vector2f(1280 / 2f, 490);
            _rect.FillColor = new Color(0, 0, 0, 160);

            _clock = new Clock();
            _clock.Restart();
        }

        public override void Update()
        {
            float sin = (float)SMath.Sin(_clock.ElapsedTime.AsMilliseconds() / 2000.0);
            double cos = SMath.Cos(_clock.ElapsedTime.AsMilliseconds() / 2000.0);
            
            _moonFront = sin < 0;
            _moon.Scale = new Vector2f((sin + 1) / 3f + 0.33f, (sin + 1) / 3f + 0.33f);

            _moon.Position = _earth.Position + new Vector2f((float)cos * 500, (float)cos * 150);

            _start.Color = new Color(255, 255, 255, (byte)(128 + 128 * SMath.Cos(_clock.ElapsedTime.AsMilliseconds() / 200.0)));
        }

        public override void PastUpdate()
        {
            
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            renderTarget.SetView(_view);
            if (_moonFront)
                renderTarget.Draw(_moon);

            renderTarget.Draw(_earth);
            renderTarget.Draw(_rect);
            renderTarget.Draw(_start);

            if(!_moonFront)
                renderTarget.Draw(_moon);




        }

        public override void Exit()
        {
            
        }
    }
}
