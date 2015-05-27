using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Input
{
    class InputEvent
    {
    }

    class KeyEvent : InputEvent
    {
        public bool Pressed { get; private set; }

        public KeyEvent(bool pressed)
        {
            Pressed = pressed;
        }
    }

    class JoystickEvent : InputEvent{
        public float Value { get; private set; }

        public JoystickEvent(float val)
        {
            Value = val;
        }
    }
}
