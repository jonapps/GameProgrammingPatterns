using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.Engine.Controller;
using JGerdesJWiemers.Game.Pong.Entities;
using SFML.Window;
using SFML.System;

namespace JGerdesJWiemers.Game.Pong.Controller
{
    class Ai : ControllerBase
    {

        private Ball _ball;

        public Ai(Window w, Paddle p, Ball b) : base(w, p)
        {
            this._ball = b;
        }

        public override float Update()
        {
            if (_paddle.Position.Y < _ball.Position.Y)
            {
                return 0.1f;
            }
            else
            {
                return -0.1f;
            }
        }

        private float _GenerateNewPosition()
        {

            return 0;
        }
    }
}
