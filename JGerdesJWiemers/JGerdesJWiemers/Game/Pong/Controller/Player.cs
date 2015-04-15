using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.Engine.Controller;
using SFML.Window;

namespace JGerdesJWiemers.Game.Pong.Controller
{
    class Player : ControllerBase
    {
        private bool _up;
        private bool _down;
        private bool _updated;

        public Player(Window w) : base(w)
        {
            this._window.KeyPressed += this._ProcessKeyboardPress;
            this._window.KeyReleased += this._ProcessKeyboardRelease;
            this._updated = false;
        }

        public override float Update()
        {
            float result = 0;
            if (_up)
            {
                result = -1; 
            }
            if (_down)
            {
                result = 1;
            }

            return result;
        }

        private void _ProcessKeyboardPress(Object sender, KeyEventArgs e)
        {
            if(e.Code == Keyboard.Key.W || e.Code == Keyboard.Key.Up){
                this._up = true;
            }
            if (e.Code == Keyboard.Key.S || e.Code == Keyboard.Key.Down)
            {
                this._down = true;
            }
        }

        private void _ProcessKeyboardRelease(Object sender, KeyEventArgs e)
        {
 
            if (e.Code == Keyboard.Key.W || e.Code == Keyboard.Key.Up)
            {
                this._up = false;
            }
            if (e.Code == Keyboard.Key.S || e.Code == Keyboard.Key.Down)
            {
                this._down = false;
            }
        }

    }
}
