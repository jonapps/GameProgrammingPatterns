using JGerdesJWiemers.Game.Engine.Audio;
using JGerdesJWiemers.Game.Engine.Input;
using JGerdesJWiemers.Game.Engine.Interfaces;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Graphics.Screens
{
    abstract class Screen : InputHandler, IDrawable
    {
        protected RenderWindow _window;
        protected ScreenManager _screenManager;
        protected InputMapper _input;

        public Screen(RenderWindow window)
        {
            _window = window;
            _input = new InputMapper();

        }

        public ScreenManager Manager
        {
            set
            {
                _screenManager = value;
            }
        }


        public virtual void Create()
        {

        }




        public abstract void Exit();

        public virtual bool DoRenderBelow()
        {
            return false;
        }
        public virtual bool DoUpdateBelow()
        {
            return false;
        }


        public virtual bool OnInputEvent(string name, InputEvent e, int channel)
        {
            return _input.OnInputEvent(name, e, channel);
        }


        public abstract void Update();

        public abstract void PastUpdate();

        public abstract void PreDraw(float extra);

        public abstract void Draw(RenderTarget target, RenderStates states);
    }
}
