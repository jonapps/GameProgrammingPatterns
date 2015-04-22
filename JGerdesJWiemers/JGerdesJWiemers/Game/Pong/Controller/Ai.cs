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

        public Ai(Paddle p, Ball b) : base(p)
        {
            this._ball = b;
        }

        public override float Update()
        {
            bool distance = Math.Abs(_paddle.Position.Y - _ball.Position.Y) > 20;
            if (distance)
            {
                if (_paddle.Position.Y < _ball.Position.Y)
                {
                    return 0.15f;
                }
                else if (_paddle.Position.Y > _ball.Position.Y)
                {
                    return -0.15f;
                }
            }
            
            return 0f;
        }

        private float _GenerateNewPosition()
        {

            return 0;
        }
    }
}
