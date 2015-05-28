using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using JGerdesJWiemers.Game.Engine.Input;
using SFML.Graphics;
using JGerdesJWiemers.Game.Engine.Graphics.Screens;

namespace JGerdesJWiemers.Game.Engine.Graphics
{
    class ScreenManager : Screen
    {
        private Stack<ScreenData> _screens;
        private class ScreenData
        {
            public bool render = true;
            public bool update = true;
            public Screen screen;

            public ScreenData(Screen s)
            {
                screen = s;
            }
        }

        
        public ScreenManager(RenderWindow w)
            : base(w){
                _screens = new Stack<ScreenData>();
                InputManager.Instance.InputHandler += _InputHandler;
        }

        bool _InputHandler(string name, InputEvent e, int channel)
        {
            for (int i = 0, c = _screens.Count; i < c; ++i)
            {
                if (_screens.ElementAt(i).screen.OnInputEvent(name, e, channel))
                {
                    return true;
                }
            }
            return false;
        }

        public void Push(Screen s)
        {
            s.Manager = this;
            if (_screens.Count > 0)
            {
                ScreenData top = _screens.Peek();
                top.render = s.DoRenderBelow();
                top.update = s.DoUpdateBelow();
            }
            _screens.Push(new ScreenData(s));
            s.Create();
        }

        public Screen Pop()
        {
            Screen old = _screens.Pop().screen;
            old.Exit();
            if (_screens.Count > 0)
            {
                ScreenData top = _screens.Peek();
                top.render = true;
                top.update = true;
            }
            return old;
        }

        public Screen Switch(Screen s)
        {
            Screen old = Pop();
            Push(s);
            return old;

        }

        public Screen Top()
        {
            return _screens.Peek().screen;
        }

        public override void Update()
        {
            for (int i = _screens.Count() - 1; i >= 0; --i)
            {
                ScreenData current = _screens.ElementAt(i);
                if (current.update)
                    current.screen.Update();
            }
        }

        public override void PastUpdate()
        {
            for (int i = _screens.Count() - 1; i >= 0; --i)
            {
                ScreenData current = _screens.ElementAt(i);
                if (current.update)
                    current.screen.PastUpdate();
            }
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            for (int i = _screens.Count() - 1; i >= 0; --i)
            {
                ScreenData current = _screens.ElementAt(i);
                if(current.render)
                    current.screen.Render(renderTarget, extra);
            }
        }


        public override void Exit()
        {
            while (_screens.Count > 0)
            {
                _screens.Pop().screen.Exit();
            }
        }
    }
}
