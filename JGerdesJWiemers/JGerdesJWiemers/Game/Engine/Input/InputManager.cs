﻿
using JGerdesJWiemers.Game.Engine.Exceptions;
using SFML.Window;
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
        public delegate void ButtonEventHandler(bool pressed);

        private static bool _isInit = false;
        private static List<Channel> _channel;

        public static void Init(Window window)
        {
            if (!_isInit)
            {
                _channel = new List<Input.Channel>();
                for (uint i = 0; i < 4; i++)
                {
                    _channel.Add(new Channel());
                }


                window.JoystickMoved += delegate(object sender, JoystickMoveEventArgs e)
                {
                    _channel[(int)e.JoystickId].HandleJoystickMoved(sender, e);
                };

                window.JoystickButtonPressed += delegate(object sender, JoystickButtonEventArgs e)
                {
                    _channel[(int)e.JoystickId].HandleJoystickButtonPressed(sender, e);
                };

                window.JoystickButtonReleased += delegate(object sender, JoystickButtonEventArgs e)
                {
                    _channel[(int)e.JoystickId].HandleJoystickButtonReleased(sender, e);
                };

                window.KeyPressed += delegate(object sender, KeyEventArgs e)
                {
                    foreach (Channel c in _channel)
                    {
                        c.HandleKeyPressed(sender, e);
                    }
                };

                window.KeyReleased += delegate(object sender, KeyEventArgs e)
                {
                    foreach (Channel c in _channel)
                    {
                        c.HandleKeyReleased(sender, e);
                    }
                };


                //TODO: Read from file
                _channel[0].KeyUp = Keyboard.Key.W;
                _channel[0].KeyDown = Keyboard.Key.S;
                _channel[0].KeyLeft = Keyboard.Key.A;
                _channel[0].KeyRight = Keyboard.Key.D;
                _channel[0].KeyAction1 = Keyboard.Key.Space;

                _channel[0].AxisUpDown = Joystick.Axis.Y;
                _channel[0].AxisLeftRight = Joystick.Axis.X;
                _channel[0].UpMax = 100;
                _channel[0].DownMax = -100;
                _channel[0].RightMax = 100;
                _channel[0].LeftMax = -100;
                _channel[0].Deadzone = 20;

                _channel[0].ShoulderLeft = 4;
                _channel[0].ShoulderRight = 5;

                _isInit = true;
            }
        }

        public static List<Channel> Channel
        {
            get
            {
                if (_isInit)
                {
                    return _channel;
                }
                else
                {
                    throw new NotInitialisatedException();
                }
            }
        }


        public static void Debug(bool debug){
            if (debug)
            {
                for (int i=0; i<_channel.Count; i++)
                {
                    Channel c = _channel[i];
                    c.OnAction1 += delegate(bool a) { System.Console.WriteLine("C" + i + ":OnAction1 " + (a ? "pressed" : "released")); };
                    c.OnAction2 += delegate(bool a) { System.Console.WriteLine("C" + i + ":OnAction2 " + (a ? "pressed" : "released")); };

                    c.OnUp += delegate(float val) { System.Console.WriteLine("C" + i + ":OnUp "+val);};
                    c.OnDown += delegate(float val) { System.Console.WriteLine("C" + i + ":OnDown "+val);};
                    c.OnLeft += delegate(float val) { System.Console.WriteLine("C" + i + ":OnLeft "+val);};
                    c.OnRight += delegate(float val) { System.Console.WriteLine("C" + i + ":OnRight " + val); };
                }
            }
        }


    }
}
