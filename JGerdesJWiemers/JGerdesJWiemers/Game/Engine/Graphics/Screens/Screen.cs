using JGerdesJWiemers.Game.Engine.Audio;
using JGerdesJWiemers.Game.Engine.EventSystem;
using JGerdesJWiemers.Game.Engine.EventSystem.Events;
using JGerdesJWiemers.Game.Engine.Input;
using JGerdesJWiemers.Game.Engine.Interfaces;
using SFML.Graphics;
using SFML.System;
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
        protected Shader _shader;
        protected View _view;
        protected Color _clearColor;
        protected bool _autoResize = true;

        public Screen(RenderWindow window)
        {
            _window = window;
            _view = new View(new FloatRect(0, 0, _window.Size.X, _window.Size.Y));
            _view.Viewport = new FloatRect(0, 0, 1, 1);
            _input = new InputMapper();
            _clearColor = new Color(255, 255, 255, 0);
            EventStream.Instance.On(JGerdesJWiemers.Game.Game.EVENT_RESIZE, _ResizeView);
        }

        private void _ResizeView(EngineEvent eventData)
        {
            if(_autoResize)
                _view.Size = (Vector2f)eventData.Data;
        }

        public ScreenManager Manager
        {
            set
            {
                _screenManager = value;
            }
        }

        public Shader Shader
        {
            get
            {
                return _shader;
            }
        }

        public Color ClearColor
        {
            get
            {
                return _clearColor;
            }
        }

        public View View
        {
            get
            {
                return _view;
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
