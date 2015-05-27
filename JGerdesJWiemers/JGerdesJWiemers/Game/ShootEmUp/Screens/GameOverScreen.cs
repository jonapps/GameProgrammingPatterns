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
using JGame = JGerdesJWiemers.Game.Game;

namespace JGerdesJWiemers.Game.ShootEmUp.Screens
{
    class GameOverScreen : Screen
    {
        View _view;
        RectangleShape _background;
        Sprite _earth;
        Sprite _spaceship;
        Text _header;
        Text _score;
        Vector2f _earthDestination;
        bool _eartIsUp = false;

        public GameOverScreen(RenderWindow w)
            : base(w)
        {
            _background = new RectangleShape(new Vector2f(1280, 720));
            _background.Position = new Vector2f(0, 0);
            _background.FillColor = new Color(0, 0, 0, 180);
            _earth = new Sprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_EARTH_TOP).Texture);
            _earth.Origin = new Vector2f(0,0);
            _earth.Position = new Vector2f(0, 720);
            _earthDestination = new Vector2f(0, 720f / 2 - 20);

            _spaceship = new Sprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_SPACESHIP_LARGE).Texture);
            _spaceship.Origin = new Vector2f(_spaceship.GetLocalBounds().Width / 2f, _spaceship.GetLocalBounds().Height / 2f);
            _spaceship.Position = new Vector2f(900, 500);

            _header = new Text("GameOver", AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_LIGHT));
            _header.CharacterSize = 48;
            _header.Origin = new Vector2f(_header.GetLocalBounds().Width / 2f, 0);
            _header.Position = new Vector2f(1280 / 2f, 32);

            _score = new Text("[SCORE]", AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_LIGHT));
            _score.CharacterSize = 32;
            _score.Origin = new Vector2f(_score.GetLocalBounds().Width / 2f, 0);
            _score.Position = new Vector2f(1280 / 2f, 128);

            Vector2f size = new Vector2f(w.Size.X, w.Size.Y);
            _view = new View(size / 2f, size);

            _input.On("return", delegate(InputEvent e, int channel)
            {
                _screenManager.Pop();
                return true;
            });

        }


        public override void Update()
        {
            if(!_eartIsUp)
            {
                _earth.Position = _earth.Position + (_earthDestination - _earth.Position) / 20f;
                _eartIsUp = _earthDestination.Distance2To(_earth.Position) < 40f;
            }
            else
            {
                _earth.Position += new Vector2f(0, 0.2f);
                _earth.Scale *= 1.0005f;
            }





            

            _spaceship.Position = new Vector2f(900, 500) + new Vector2f((float)SMath.Cos(JGame.ElapsedTime / 500f), (float)SMath.Sin(JGame.ElapsedTime / 500f)) * 10f;
            
        }

        public override void PastUpdate()
        {

        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            renderTarget.SetView(_view);
            renderTarget.Draw(_background);
            renderTarget.Draw(_earth);
            renderTarget.Draw(_header);
            renderTarget.Draw(_score);
            renderTarget.Draw(_spaceship);
        }

        public override void Exit()
        {

        }

        public override bool OnInputEvent(string name, InputEvent e, int channel)
        {
            base.OnInputEvent(name, e, channel);
            return true; //consume all inputs
        }

        public override bool DoRenderBelow()
        {
            return true;
        }


    }
}
