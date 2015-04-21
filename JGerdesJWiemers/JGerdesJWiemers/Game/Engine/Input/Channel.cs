using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Input
{
    class Channel
    {
        struct Data
        {
            Keyboard.Key KeyUp;
            Keyboard.Key KeyDown;
            Keyboard.Key KeyLeft;
            Keyboard.Key KeyRight;

            Keyboard.Key KeyAction1;
            Keyboard.Key KeyAction2;

            Joystick.Axis AxisUpDown;
            Joystick.Axis AxisLeftRight;
            float UpMax;
            float DownMax;
            float LeftMax;
            float RightMax;

            uint ShoulderLeft;
            uint ShoulderRight;
            uint Action1;
            uint Action2;
        }

    }
}
