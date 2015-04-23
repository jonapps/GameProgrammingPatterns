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
        public static float DIFFICULTY_EASY = 0.15f;
        public static float DIFFICULTY_MID = 0.15f;
        public static float DIFFICULTY_HARD = 0.15f;

        private float _difficulty;

        private Ball _ball;

        public Ai(Paddle p, Ball b) : base(p)
        {
            this._ball = b;
            _difficulty = DIFFICULTY_EASY;
        }

        public override float Update()
        {
            bool distance = Math.Abs(_paddle.Position.Y - _ball.Position.Y) > 20;
            if (distance)
            {
                if (_paddle.Position.Y < _ball.Position.Y)
                {
                    return _difficulty;
                }
                else if (_paddle.Position.Y > _ball.Position.Y)
                {
                    return -_difficulty;
                }
            }
            else
            {
                return _difficulty * (_ball.Position.Y - _paddle.Position.Y) / 20f;
            }
            
            return 0f;
        }

        private float _GenerateNewPosition()
        {

            return 0;
        }
    }
}
