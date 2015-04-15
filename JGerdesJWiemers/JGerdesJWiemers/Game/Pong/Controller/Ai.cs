using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.Engine.Controller;
using JGerdesJWiemers.Game.Pong.Entities;
using SFML.Window;

namespace JGerdesJWiemers.Game.Pong.Controller
{
    class Ai : ControllerBase
    {

        private Ball _ball;

        public Ai(Window w, Ball b) : base(w)
        {
            this._ball = b;
        }

        public override float Update()
        {
            return this._GenerateNewPosition();
        }

        private float _GenerateNewPosition()
        {

            return 0;
        }
    }
}
