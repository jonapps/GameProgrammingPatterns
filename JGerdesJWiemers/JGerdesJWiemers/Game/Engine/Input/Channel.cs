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


        public event InputManager.MotionEventHandler OnUp = delegate {};
        public event InputManager.MotionEventHandler OnDown = delegate {};
        public event InputManager.MotionEventHandler OnLeft = delegate {};
        public event InputManager.MotionEventHandler OnRight = delegate {};

        public event InputManager.ButtonEventHandler OnAction1 = delegate {};
        public event InputManager.ButtonEventHandler OnAction2 = delegate {};

        public void HandleJoystickMoved(object sender, JoystickMoveEventArgs e)
        {
            if (e.Position >= Deadzone)
            {
                if (e.Axis == AxisUpDown)
                {
                    if (Math.Sign(e.Position) == Math.Sign(UpMax))
                    {
                        OnUp(Math.Abs(e.Position * 100 / UpMax));
                    }
                    else
                    {
                        OnDown(Math.Abs(e.Position * 100 / DownMax));
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

        internal void HandleKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == KeyUp)
            {
                OnUp(1);
            }
            else if (e.Code == KeyDown)
            {
                OnDown(1);
            }
            else if (e.Code == KeyLeft)
            {
                OnLeft(1);
            }
            else if (e.Code == KeyRight)
            {
                OnRight(1);
            }
            else if (e.Code == KeyAction1)
            {
                OnAction1(true);
            }
            else if (e.Code == KeyAction2)
            {
                OnAction2(true);
            }
        }

        internal void HandleKeyReleased(object sender, KeyEventArgs e)
        {
            if (e.Code == KeyUp)
            {
                OnUp(0);
            }
            else if (e.Code == KeyDown)
            {
                OnDown(0);
            }
            else if (e.Code == KeyLeft)
            {
                OnLeft(0);
            }
            else if (e.Code == KeyRight)
            {
                OnRight(0);
            }
            else if (e.Code == KeyAction1)
            {
                OnAction1(false);
            }
            else if (e.Code == KeyAction2)
            {
                OnAction2(false);
            }
        }

        internal void HandleJoystickButtonPressed(object sender, JoystickButtonEventArgs e)
        {
            if (e.Button == Action1)
            {
	            OnAction1(true);
            }
            else if (e.Button == Action2)
            {
                OnAction2(true);
            }	
        }

        internal void HandleJoystickButtonReleased(object sender, JoystickButtonEventArgs e)
        {
            if (e.Button == Action1)
            {
                OnAction1(false);
            }
            else if (e.Button == Action2)
            {
                OnAction2(false);
            }
        }
    }
}
