using JGerdesJWiemers.Game.Engine.Graphics.Screens;
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
        RectangleShape _background;

        public EarthScreen(RenderWindow w)
            :base(w){
                _window.KeyPressed += window_KeyPressed;
                _background = new RectangleShape(new Vector2f(1280, 720));
                _background.Position = new Vector2f(0, 0);
                _background.FillColor = new Color(0, 0, 0, 160);

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
            renderTarget.Draw(_background);
        }

        public override void Exit()
        {
            _window.KeyPressed -= window_KeyPressed;
        }


    }
}
