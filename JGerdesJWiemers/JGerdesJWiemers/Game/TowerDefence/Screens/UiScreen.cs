using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.TowerDefence.Entities;
using JGerdesJWiemers.Game.TowerDefence.Logic;
using JGerdesJWiemers.Game.Engine.Interfaces;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.Screens
{
    class UiScreen : Screen
    {

        private Builder _builder;

        public UiScreen(RenderWindow w, Map map, ICoordsConverter converter)
            :base(w)
        {
            _builder = new Builder(map, converter);

            _window.KeyPressed += _window_KeyPressed;
            _window.MouseButtonPressed += _window_MouseButtonPressed;
        }

        void _window_MouseButtonPressed(object sender, SFML.Window.MouseButtonEventArgs e)
        {
            if (e.Button == SFML.Window.Mouse.Button.Left)
            {
                _builder.Build();
            }
        }

        void _window_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            switch (e.Code)
            {
                case SFML.Window.Keyboard.Key.Num1:
                    _builder.Selection = new Tower.Def();
                    break;
                case SFML.Window.Keyboard.Key.Num2:
                    _builder.Selection = new Tower.Def{
                        Base = new Color(100, 100, 100),
                        TopActive = new Color(240, 30, 220),
                        TopWaiting = new Color(180, 10, 160)
                    };
                    break;
                default:
                    _builder.Selection = null;
                    break;
            }
                

        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            _builder.Update();
        }

        public override void PastUpdate()
        {
            _builder.PastUpdate();
        }

        public override void PreDraw(float extra)
        {
            _builder.PreDraw(extra);
        }

        public override void Draw(SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states)
        {
            target.Draw(_builder, states);
        }

        public override bool DoRenderBelow()
        {
            return true;
        }

        public override bool DoUpdateBelow()
        {
            return true;
        }
    }
}
