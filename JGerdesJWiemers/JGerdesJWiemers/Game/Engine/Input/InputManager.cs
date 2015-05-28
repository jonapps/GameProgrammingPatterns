
using JGerdesJWiemers.Game.Engine.Exceptions;
using JGerdesJWiemers.Game.Engine.Utils;
using Newtonsoft.Json;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMath = System.Math;

namespace JGerdesJWiemers.Game.Engine.Input
{

    class InputManager
    {
        private static InputManager _instance;

        public delegate bool OnInputEvent(string name, InputEvent e, int channel);

        private Window _window;
        private InputConfig _config;
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

            String data = AssetLoader.Instance.ReadConfig(AssetLoader.CONFIG_INPUT);
            _config = new InputConfig(JsonConvert.DeserializeObject<InputConfig.JsonFormat>(data));
        }

        public void SaveConfig()
        {
            String data = JsonConvert.SerializeObject(new InputConfig.JsonFormat(_config));
            AssetLoader.Instance.WriteConfig(AssetLoader.CONFIG_INPUT, data);
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
                    if (pressed)
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
            if(e.Axis == _config.Vertical.Axis)
            {
                if (e.Position <= 0)
                    InputHandler("up", new JoystickEvent(SMath.Abs(e.Position) / 100f), (int)e.JoystickId);
                else
                    InputHandler("down", new JoystickEvent(SMath.Abs(e.Position) / 100f), (int)e.JoystickId);
            } 
            else if(e.Axis == _config.Horizontal.Axis) 
            {
                if (e.Position <= 0)
                    InputHandler("left", new JoystickEvent(SMath.Abs(e.Position) / 100f), (int)e.JoystickId);
                else
                    InputHandler("right", new JoystickEvent(SMath.Abs(e.Position) / 100f), (int)e.JoystickId);
            } 
            else if(e.Axis == _config.RotationHorizontal.Axis) 
            {
                if (e.Position <= 0)
                    InputHandler("rotLeft", new JoystickEvent(SMath.Abs(e.Position) / 100f), (int)e.JoystickId);
                else
                    InputHandler("rotRight", new JoystickEvent(SMath.Abs(e.Position) / 100f), (int)e.JoystickId);
            } 
            else if(e.Axis == _config.RotationVertical.Axis)
            {
                if (e.Position <= 0)
                    InputHandler("rotUp", new JoystickEvent(SMath.Abs(e.Position) / 100f), (int)e.JoystickId);
                else
                    InputHandler("rotDown", new JoystickEvent(SMath.Abs(e.Position) / 100f), (int)e.JoystickId);
            }
        }

        void _joystick(bool pressed, object sender, JoystickButtonEventArgs e)
        {
            if (e.Button == _config.Shoot)
            {
                InputHandler("shoot", new KeyEvent(pressed), (int)e.JoystickId);
            }
            else if (e.Button == _config.WeaponSwitch && pressed)
            {
                InputHandler("weaponSwitch", new KeyEvent(pressed), (int)e.JoystickId);
            }
            else if (e.Button == _config.Return && pressed)
            {
                InputHandler("return", new KeyEvent(pressed), (int)e.JoystickId);
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
