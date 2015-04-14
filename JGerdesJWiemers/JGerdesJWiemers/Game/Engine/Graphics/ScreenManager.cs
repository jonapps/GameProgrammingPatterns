using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Graphics
{
    class ScreenManager : IScreen
    {
        private IScreen _currentScreen;

        public IScreen CurrentScreen
        {
            get
            {
                return _currentScreen;
            }

            set
            {
                if (value != null)
                {
                    _currentScreen = value;
                }
            }
        }

        public void Update()
        {
            _currentScreen.Update();
        }

        public void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            _currentScreen.Render(renderTarget, extra);
        }
    }
}
