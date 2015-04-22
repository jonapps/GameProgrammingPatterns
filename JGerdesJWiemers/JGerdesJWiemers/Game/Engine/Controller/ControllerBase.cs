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

        public ControllerBase(Paddle paddle)
        {
            this._direction = 0;
            _paddle = paddle;
        }
        
        public abstract float Update();
    }
}
