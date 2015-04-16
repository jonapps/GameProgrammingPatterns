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

        public Ai(Window w, Ball b) : base(w)
        {
            this._ball = b;
        }

        public override Vector2f Update()
        {
            return new Vector2f(0,0) ;
        }

        private float _GenerateNewPosition()
        {

            return 0;
        }
    }
}
