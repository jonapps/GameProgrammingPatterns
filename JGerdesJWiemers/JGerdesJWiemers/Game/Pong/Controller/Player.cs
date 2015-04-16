using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using JGerdesJWiemers.Game.Engine.Controller;
using JGerdesJWiemers.Game.Pong.Entities;
using SFML.Window;
using SFML.System;

namespace JGerdesJWiemers.Game.Pong.Controller
{
    class Player : ControllerBase
    {
        public static readonly float CONTROLLER_DEADZONE = 20;
        public static readonly float CONTROLLER_MAX_INPUT = 100;
        public static readonly long MILLISECONDS_TO_FULL_SPEED = 1000;

        private float _controllerMovement = 0;
        private float _keyMovement = 0;

        private Stopwatch _stopwatch;

        //true is up, false is down
        private bool _up;

        public Player(Window w, Paddle p) : base(w, p)
        {
            _window.KeyPressed += _ProcessKeyboardPress;
            _window.KeyReleased += _ProcessKeyboardRelease;
            _window.JoystickMoved += _ProcessControllerMovement;
            _window.JoystickButtonPressed += _ProcessControllerPress;
            _window.JoystickButtonReleased += _ProcessControllerReleased;
            _stopwatch = new Stopwatch();
        }

        private void _ProcessControllerReleased(object sender, JoystickButtonEventArgs e)
        {
            if (e.Button == 4 || e.Button == 5)
            {
                _paddle.Rotation = 0;
            }
        }

        private void _ProcessControllerPress(object sender, JoystickButtonEventArgs e)
        {
            if (e.Button == 4)
            {
                _paddle.Rotation = -60;
            }

            if (e.Button == 5)
            {
                _paddle.Rotation = 60;
            }
        }

        private void _ProcessControllerMovement(object sender, JoystickMoveEventArgs e)
        {
            if (e.Axis == Joystick.Axis.Y)
            {
                _controllerMovement = Math.Sign(e.Position) * e.Position * e.Position - CONTROLLER_DEADZONE;
                _controllerMovement /= CONTROLLER_MAX_INPUT * CONTROLLER_MAX_INPUT;
                
            }


        }


        public override float Update()
        {
            if (_stopwatch.ElapsedMilliseconds != 0)
            {
                float amount = _stopwatch.ElapsedMilliseconds / (float)MILLISECONDS_TO_FULL_SPEED;
                amount *= 2;
                if (amount > 1)
                    amount = 1;

                _keyMovement = _up ? -amount : amount;
            }
            float currentMovement = _controllerMovement != 0 ? _controllerMovement : _keyMovement;
            _keyMovement /= 1.2f;
            return currentMovement;
        }

        private void _ProcessKeyboardPress(Object sender, KeyEventArgs e)
        {
            if(e.Code == Keyboard.Key.W || e.Code == Keyboard.Key.Up)
            {
                _stopwatch.Start();
                _up = true;
            }
            if (e.Code == Keyboard.Key.S || e.Code == Keyboard.Key.Down)
            {
                _stopwatch.Start();
                _up = false;
            }
            if (e.Code == Keyboard.Key.A || e.Code == Keyboard.Key.Left)
            {
                _paddle.Rotation = -60;
            }

            if (e.Code == Keyboard.Key.D || e.Code == Keyboard.Key.Right)
            {
                _paddle.Rotation = 60;
            }
        }

        private void _ProcessKeyboardRelease(Object sender, KeyEventArgs e)
        {
 
            if (e.Code == Keyboard.Key.W || e.Code == Keyboard.Key.Up || e.Code == Keyboard.Key.S || e.Code == Keyboard.Key.Down)
            {
                _stopwatch.Stop();
                _stopwatch.Reset();
            }

            if (e.Code == Keyboard.Key.A || e.Code == Keyboard.Key.Left || e.Code == Keyboard.Key.D || e.Code == Keyboard.Key.Right)
            {
                _paddle.Rotation = 0;
            }
        }


    }
}
