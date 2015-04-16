using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Pong.Entities;

namespace JGerdesJWiemers.Game.Engine.Controller
{
    abstract class ControllerBase
    {

        protected float _direction;
        protected Paddle _paddle;
        protected Window _window;

        public ControllerBase(Window w, Paddle paddle)
        {
            this._window = w;
            this._direction = 0;
            _paddle = paddle;
        }
        
        public abstract float Update();
    }
}
