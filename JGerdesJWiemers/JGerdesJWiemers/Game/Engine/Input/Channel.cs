using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Input
{
    class Channel
    {

        private static readonly float MAX_PRESSED_TIME = 1500;


        public Keyboard.Key KeyUp;
        public Keyboard.Key KeyDown;
        public Keyboard.Key KeyLeft;
        public Keyboard.Key KeyRight;

        public Keyboard.Key KeyAction1;
        public Keyboard.Key KeyAction2;
        public Keyboard.Key KeyAction3;
        public Keyboard.Key KeyAction4;

        public Joystick.Axis AxisUpDown;
        public Joystick.Axis AxisLeftRight;
        public float UpMax;
        public float DownMax;
        public float LeftMax;
        public float RightMax;
        public float Deadzone;

        
        public uint Action1;
        public uint Action2;
        public uint Action3;
        public uint Action4;

        private Stopwatch _upWatch;
        private Stopwatch _downWatch;
        private Stopwatch _leftWatch;
        private Stopwatch _rightWatch;


        public event InputManager.MotionEventHandler OnUp = delegate {};
        public event InputManager.MotionEventHandler OnDown = delegate {};
        public event InputManager.MotionEventHandler OnLeft = delegate {};
        public event InputManager.MotionEventHandler OnRight = delegate {};

        public event InputManager.ButtonEventHandler OnAction1 = delegate {};
        public event InputManager.ButtonEventHandler OnAction2 = delegate { };
        public event InputManager.ButtonEventHandler OnAction3 = delegate { };
        public event InputManager.ButtonEventHandler OnAction4 = delegate { };

        public Channel()
        {
            _upWatch = new Stopwatch();
            _downWatch = new Stopwatch();
            _leftWatch = new Stopwatch();
            _rightWatch = new Stopwatch();
        }

        public void ResetEvents()
        {
            OnUp = delegate { };
            OnDown = delegate { };
            OnLeft = delegate { };
            OnRight = delegate { };

            OnAction1 = delegate { };
            OnAction2 = delegate { };
            OnAction3 = delegate { };
            OnAction4 = delegate { };
        }

        public void HandleJoystickMoved(object sender, JoystickMoveEventArgs e)
        {
            
            if (e.Axis == AxisUpDown)
            {
                if (Math.Sign(e.Position) == Math.Sign(UpMax))
                {
                    if (Math.Abs(e.Position) >= Deadzone)
                    {
                        OnUp(Math.Abs(e.Position / UpMax));
                    }
                    else
                    {
                        OnUp(0);
                    }
                }
                else
                {
                    if (Math.Abs(e.Position) >= Deadzone)
                    {
                        OnDown(Math.Abs(e.Position / DownMax));
                    }
                    else
                    {
                        OnDown(0);
                    }
                }
            }
            else if(e.Axis == AxisLeftRight)
            {
                if (Math.Sign(e.Position) == Math.Sign(LeftMax))
                {
                    OnLeft(e.Position / LeftMax);
                }
                else
                {
                    OnRight(e.Position / RightMax);
                }
            }

        }

        internal void HandleKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == KeyUp)
            {
                if(_upWatch.IsRunning)
                {
                    OnUp(Math.Min(0.1f+_upWatch.ElapsedMilliseconds/MAX_PRESSED_TIME,1));
                }
                else
                {
                    _upWatch.Start();
                    OnUp(0.3f);    
                }
            }
            if (e.Code == KeyDown)
            {
                if(_downWatch.IsRunning)
                {
                    OnDown(Math.Min(0.1f + _downWatch.ElapsedMilliseconds / MAX_PRESSED_TIME, 1));
                }
                else
                {
                    _downWatch.Start();
                    OnDown(0.3f);    
                }
            }
            if (e.Code == KeyLeft)
            {
                if(_leftWatch.IsRunning)
                {
                    OnLeft(Math.Min(0.1f + _leftWatch.ElapsedMilliseconds / MAX_PRESSED_TIME, 1));
                }
                else
                {
                    _leftWatch.Start();
                    OnLeft(0.3f);    
                }
            }
            if (e.Code == KeyRight)
            {
                if(_rightWatch.IsRunning)
                {
                    OnRight(Math.Min(0.1f + _rightWatch.ElapsedMilliseconds / MAX_PRESSED_TIME, 1));
                }
                else
                {
                    _rightWatch.Start();
                    OnRight(0.3f);    
                }
            }
            if (e.Code == KeyAction1)
            {
                OnAction1(true);
            }
            if (e.Code == KeyAction2)
            {
                OnAction2(true);
            }
            if (e.Code == KeyAction3)
            {
                OnAction3(true);
            }
            if (e.Code == KeyAction4)
            {
                OnAction4(true);
            }
        }

        internal void HandleKeyReleased(object sender, KeyEventArgs e)
        {
            if (e.Code == KeyUp)
            {
                OnUp(0);
                _upWatch.Stop();
                _upWatch.Reset();
            }
            if (e.Code == KeyDown)
            {
                OnDown(0);
                _downWatch.Stop();
                _downWatch.Reset();
            }
            if (e.Code == KeyLeft)
            {
                OnLeft(0);
                _leftWatch.Stop();
                _leftWatch.Reset();
            }
            if (e.Code == KeyRight)
            {
                OnRight(0);
                _rightWatch.Stop();
                _rightWatch.Reset();
            }
            if (e.Code == KeyAction1)
            {
                OnAction1(false);
            }
            if (e.Code == KeyAction2)
            {
                OnAction2(false);
            }
            if (e.Code == KeyAction3)
            {
                OnAction3(false);
            }
            if (e.Code == KeyAction4)
            {
                OnAction4(false);
            }
        }

        internal void HandleJoystickButtonPressed(object sender, JoystickButtonEventArgs e)
        {
            if (e.Button == Action1)
            {
	            OnAction1(true);
            }
            if (e.Button == Action2)
            {
                OnAction2(true);
            }
            if (e.Button == Action3)
            {
                OnAction3(true);
            }
            if (e.Button == Action4)
            {
                OnAction4(true);
            }
        }

        internal void HandleJoystickButtonReleased(object sender, JoystickButtonEventArgs e)
        {
            if (e.Button == Action1)
            {
                OnAction1(false);
            }
            if (e.Button == Action2)
            {
                OnAction2(false);
            }
            if (e.Button == Action3)
            {
                OnAction3(false);
            }
            if (e.Button == Action4)
            {
                OnAction4(false);
            }
        }
    }
}
