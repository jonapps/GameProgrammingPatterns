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
    class EarthScreen : Screen
    {
        View _view;
        RectangleShape _background;
        Sprite _earth;

        public EarthScreen(RenderWindow w)
            :base(w){
            _window.KeyPressed += window_KeyPressed;
            _background = new RectangleShape(new Vector2f(1280, 720));
            _background.Position = new Vector2f(0, 0);
            _background.FillColor = new Color(0, 0, 0, 160);
            _earth = new Sprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_EARTH_BIG).Texture);
            _earth.Origin = new Vector2f(0, _earth.Texture.Size.Y / 2f);
            _earth.Position = new Vector2f(0, 720 / 2f);

            Vector2f size = new Vector2f(w.Size.X, w.Size.Y);
            _view = new View(size / 2f,size);

        }

        void window_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            if (e.Code == SFML.Window.Keyboard.Key.Return)
            {
                _screenManager.Pop();
            }
        }

        public override void Update()
        {
           
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            renderTarget.SetView(_view);
            renderTarget.Draw(_background);
            renderTarget.Draw(_earth);
        }

        public override void Exit()
        {
            _window.KeyPressed -= window_KeyPressed;
        }


    }
}
