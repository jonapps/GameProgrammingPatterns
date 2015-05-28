
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
        private static InputManager _instance;

        public delegate bool OnInputEvent(string name, InputEvent e, int channel);

        private Window _window;
        public event OnInputEvent InputHandler;

        public void Init(Window w)
        {
            _window = w;
            _window.JoystickButtonPressed += delegate(object sender, JoystickButtonEventArgs e)
            {
                _joystick(true, sender, e);
            };

            _window.JoystickButtonReleased += delegate(object sender, JoystickButtonEventArgs e)
            {
                _joystick(false, sender, e);
            };

            _window.JoystickMoved += _JoystickMoved;

            _window.KeyPressed += delegate(object sender, KeyEventArgs e)
            {
                _key(true, sender, e);
            };

            _window.KeyReleased += delegate(object sender, KeyEventArgs e)
            {
                _key(false, sender, e);
            };

        }

        void _key(bool pressed, object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.W:
                    InputHandler("up", new JoystickEvent(pressed ? 1 : 0), 0);
                    break;
                case Keyboard.Key.S:
                    InputHandler("down", new JoystickEvent(pressed ? 1 : 0), 0);
                    break;
                case Keyboard.Key.A:
                    InputHandler("left", new JoystickEvent(pressed ? 1 : 0), 0);
                    break;
                case Keyboard.Key.D:
                    InputHandler("right", new JoystickEvent(pressed ? 1 : 0), 0);
                    break;
                case Keyboard.Key.Up:
                    InputHandler("rotUp", new JoystickEvent(pressed ? 1 : 0), 0);
                    break;
                case Keyboard.Key.Down:
                    InputHandler("rotDown", new JoystickEvent(pressed ? 1 : 0), 0);
                    break;
                case Keyboard.Key.Left:
                    InputHandler("rotLeft", new JoystickEvent(pressed ? 1 : 0), 0);
                    break;
                case Keyboard.Key.Right:
                    InputHandler("rotRight", new JoystickEvent(pressed ? 1 : 0), 0);
                    break;
                case Keyboard.Key.L:
                    InputHandler("land", new KeyEvent(pressed), 0);
                    break;
                case Keyboard.Key.X:
                    InputHandler("weaponSwitch", new KeyEvent(pressed), 0);
                    break;
                case Keyboard.Key.Space:
                    InputHandler("shoot", new KeyEvent(pressed), 0);
                    break;
                case Keyboard.Key.Return:
                    if(pressed)
                        InputHandler("return", new KeyEvent(pressed), 0);
                    break;
            }
        }

  
        void _JoystickMoved(object sender, JoystickMoveEventArgs e)
        {
            Console.WriteLine(e.ToString());
            switch (e.Axis)
            {
                case Joystick.Axis.Y:
                    if (e.Position <= 0)
                        InputHandler("up", new JoystickEvent(Math.Abs(e.Position) / 100f), (int)e.JoystickId);
                    else
                        InputHandler("down", new JoystickEvent(Math.Abs(e.Position) / 100f), (int)e.JoystickId);
                    break;
                case Joystick.Axis.X:
                    if (e.Position <= 0)
                        InputHandler("left", new JoystickEvent(Math.Abs(e.Position) / 100f), (int)e.JoystickId);
                    else
                        InputHandler("right", new JoystickEvent(Math.Abs(e.Position) / 100f), (int)e.JoystickId);
                    break;
                case Joystick.Axis.R:
                    if (e.Position <= 0)
                        InputHandler("rotLeft", new JoystickEvent(Math.Abs(e.Position) / 100f), (int)e.JoystickId);
                    else
                        InputHandler("rotRight", new JoystickEvent(Math.Abs(e.Position) / 100f), (int)e.JoystickId);
                    break;
                case Joystick.Axis.Z:
                    if (e.Position <= 0)
                        InputHandler("rotUp", new JoystickEvent(Math.Abs(e.Position) / 100f), (int)e.JoystickId);
                    else
                        InputHandler("rotDown", new JoystickEvent(Math.Abs(e.Position) / 100f), (int)e.JoystickId);
                    break;
            }
        }

        void _joystick(bool pressed, object sender, JoystickButtonEventArgs e)
        {
            switch (e.Button)
            {
                case 3:
                    InputHandler("land", new KeyEvent(pressed), (int)e.JoystickId);
                    break;
                case 5:
                    InputHandler("weaponSwitch", new KeyEvent(pressed), (int)e.JoystickId);
                    break;
                case 4:
                    InputHandler("shoot", new KeyEvent(pressed), (int)e.JoystickId);
                    break;
                case 9:
                    if(pressed)
                        InputHandler("return", new KeyEvent(pressed), (int)e.JoystickId);
                    break;
            }
        }

        public static InputManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InputManager();
                }
                return _instance;
            }
        }


    }
}
