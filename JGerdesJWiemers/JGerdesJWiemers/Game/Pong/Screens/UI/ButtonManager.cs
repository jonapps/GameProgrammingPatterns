using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Pong.Screens.UI
{
  
    class ButtonManager : IUpdateable, IRenderable
    {
        private List<Button> _buttons;
        private int _currentSelection;

        public ButtonManager(Window w)
        {
            
        }



        public void Update()
        {
            foreach (Button b in _buttons)
            {
                b.Update();
            }
        }

        public void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            foreach (Button b in _buttons)
            {
                b.Render(renderTarget, extra);
            }
        }
    }
}
