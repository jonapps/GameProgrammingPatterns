using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using JGerdesJWiemers.Game.Engine.Input;
using SFML.Graphics;

namespace JGerdesJWiemers.Game.Engine.Graphics
{
    class ScreenManager : Screen
    {
        private Screen _currentScreen;

        public Screen CurrentScreen
        {
            get
            {
                return _currentScreen;
            }

            set
            {
                if (value != null)
                {
                    if(_currentScreen != null){
                        _currentScreen.Exit();
                    }
                    _currentScreen = value;
                    _currentScreen.Manager = this;
                }
            }
        }

        public ScreenManager(RenderWindow w)
            : base(w)
        {

        }

        public override void Update()
        {
            _currentScreen.Update();
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            _currentScreen.Render(renderTarget, extra);
        }


        public override void Exit()
        {
            _currentScreen.Exit();
        }
    }
}
