﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using JGerdesJWiemers.Game.Engine.Input;
using SFML.Graphics;
using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using SFML.System;
using JGerdesJWiemers.Game.Engine.EventSystem;

namespace JGerdesJWiemers.Game.Engine.Graphics
{
    class ScreenManager : Screen
    {
        private Stack<ScreenData> _screens;

        private RenderTexture _renderTexture;
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
                _renderTexture = new RenderTexture(_window.Size.X, _window.Size.Y);
                _view = new View(new FloatRect(0, 0, _window.Size.X, _window.Size.Y));
                _view.Viewport = new FloatRect(0, 0, 1, 1);
                _window.SetView(_view);
                _renderTexture.Display();

                EventStream.Instance.OnPersistent(Game.EVENT_RESIZE, _Resize);
        }

        private void _Resize(EventSystem.Events.EngineEvent eventData)
        {
            Vector2f sizef = (Vector2f)eventData.Data;
            Vector2u size = new Vector2u((uint)sizef.X , (uint)sizef.Y);
            _renderTexture.Dispose();
            _renderTexture = new RenderTexture(size.X, size.Y);
            _renderTexture.Smooth = true;
            _view = new View(new FloatRect(0, 0, sizef.X, sizef.Y));
            _view.Viewport = new FloatRect(0, 0, 1, 1);
            _window.SetView(_view);
            _renderTexture.Display();
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

        private void _RevalidateStates()
        {
            bool render = true;
            bool update = true;

            ScreenData last = _screens.Peek();
            last.render = render;
            last.update = update;
            ScreenData current;

            for (int i = 1, c = _screens.Count; i < c; i++)
            {
                current = _screens.ElementAt(i);
                current.render = render = render && last.screen.DoRenderBelow();
                current.update = update = update && last.screen.DoUpdateBelow();
                last = current;
            }
        }

        private void _Push(Screen s, bool revalidate)
        {
            s.Manager = this;
            _screens.Push(new ScreenData(s));
            if (revalidate)
                _RevalidateStates();
            s.Create();
        }
        
        public void Push(Screen s)
        {
            _Push(s, true);
        }

        public Screen Pop()
        {
            Screen old = _screens.Pop().screen;
            old.Exit();
            if (_screens.Count > 0)
                _RevalidateStates();
            return old;
        }

        public void PopTo(Screen screen)
        {
            if (Contains(screen))
            {
                while (_screens.Peek().screen != screen)
                {
                    Pop();
                }
            }
        }

        public bool Contains(Screen screen)
        {
            foreach (ScreenData sd in _screens)
            {
                if (sd.screen == screen)
                {
                    return true;
                }
            }
            return false;
        }

        public Screen Switch(Screen s)
        {
            Screen old = Pop();
            _Push(s, false);
            return old;
        }

        public void RemoveAll()
        {
            _screens.Clear();
        }

        public Screen Top()
        {
            if (_screens.Count > 0)
                return _screens.Peek().screen;
            else
                return null;
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

        public override void PreDraw(float extra)
        {
            for (int i = _screens.Count() - 1; i >= 0; --i)
            {
                ScreenData current = _screens.ElementAt(i);
                if (current.render)
                    current.screen.PreDraw(extra);
            }
        }

        public override void Draw(SFML.Graphics.RenderTarget renderTarget, RenderStates states)
        {
            for (int i = _screens.Count() - 1; i >= 0; --i)
            {
                ScreenData current = _screens.ElementAt(i);
                if (current.render)
                {
                    _renderTexture.Clear(current.screen.ClearColor);
                    _renderTexture.SetView(current.screen.View);
                    _renderTexture.Draw(current.screen, states);

                    RenderStates screenStates = new RenderStates(states);
                    //screenStates.BlendMode = BlendMode.Alpha;
                    if (current.screen.Shader != null)
                    {
                        screenStates.Shader = current.screen.Shader;
                    }
                    renderTarget.Draw(new Sprite(_renderTexture.Texture), screenStates);
                }
                    
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
