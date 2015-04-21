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
        public Keyboard.Key KeyUp;
        public Keyboard.Key KeyDown;
        public Keyboard.Key KeyLeft;
        public Keyboard.Key KeyRight;

        public Keyboard.Key KeyAction1;
        public Keyboard.Key KeyAction2;

        public Joystick.Axis AxisUpDown;
        public Joystick.Axis AxisLeftRight;
        public float UpMax;
        public float DownMax;
        public float LeftMax;
        public float RightMax;
        public float Deadzone;

        public uint ShoulderLeft;
        public uint ShoulderRight;
        public uint Action1;
        public uint Action2;


        public event InputManager.MotionEventHandler OnUp;
        public event InputManager.MotionEventHandler OnDown;
        public event InputManager.MotionEventHandler OnLeft;
        public event InputManager.MotionEventHandler OnRight;

        public event InputManager.ButtonEventHandler OnAction1;

        public void HandleJoystickMoved(object sender, JoystickMoveEventArgs e)
        {
            if (e.Position >= Deadzone)
            {
                if (e.Axis == AxisUpDown)
                {
                    if (Math.Sign(e.Position) == Math.Sign(UpMax))
                    {
                        OnUp(e.Position * 100 / UpMax);
                    }
                    else
                    {
                        OnDown(e.Position * 100 / DownMax);
                    }
                }
                else if(e.Axis == AxisLeftRight)
                {
                    if (Math.Sign(e.Position) == Math.Sign(LeftMax))
                    {
                        OnLeft(e.Position * 100 / LeftMax);
                    }
                    else
                    {
                        OnRight(e.Position * 100 / RightMax);
                    }
                }
            }
        }
    }
}
