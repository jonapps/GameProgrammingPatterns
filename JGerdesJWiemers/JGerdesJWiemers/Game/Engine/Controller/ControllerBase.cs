using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Controller
{
    abstract class ControllerBase
    {

        protected float _direction;
        protected Window _window;

        public ControllerBase(Window w)
        {
            this._window = w;
            this._direction = 0;
        }
        
        public abstract float Update();
    }
}
