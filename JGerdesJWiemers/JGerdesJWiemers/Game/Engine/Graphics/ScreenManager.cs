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
        private Stack<Screen> _screens;

        
        public ScreenManager(RenderWindow w)
            : base(w){
                _screens = new Stack<Screen>();
        }

        public void Push(Screen s)
        {
            s.Manager = this;
            _screens.Push(s);
        }

        public Screen Pop()
        {
            Screen old = _screens.Pop();
            old.Exit();
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
            return _screens.Peek();
        }

        public override void Update()
        {
            _screens.Peek().Update();
        }

        public override void PastUpdate()
        {
            _screens.Peek().PastUpdate();
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            for (int i = _screens.Count() - 1; i >= 0; --i)
                _screens.ElementAt(i).Render(renderTarget, extra);
        }


        public override void Exit()
        {
            while (_screens.Count > 0)
            {
                _screens.Pop().Exit();
            }
        }
    }
}
