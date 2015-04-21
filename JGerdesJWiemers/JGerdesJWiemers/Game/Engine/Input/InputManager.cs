
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Input
{

    class InputManager
    {

        public delegate void MotionEventHandler(float value);
        public delegate void ButtonEventHandler();

        private List<Channel> _channel;

    }
}
