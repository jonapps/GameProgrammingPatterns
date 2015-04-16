using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using JGerdesJWiemers.Game.Engine.Controller;
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

        private float _rotation;
        private Stopwatch _stopwatch;

        //true is up, false is down
        private bool _up;

        public Player(Window w) : base(w)
        {
            _window.KeyPressed += _ProcessKeyboardPress;
            _window.KeyReleased += _ProcessKeyboardRelease;
            _window.JoystickMoved += _ProcessControllerInput;
            _stopwatch = new Stopwatch();
        }

        private void _ProcessControllerInput(object sender, JoystickMoveEventArgs e)
        {
            if (e.Axis == Joystick.Axis.Y)
            {
                _controllerMovement = Math.Sign(e.Position) * e.Position * e.Position - CONTROLLER_DEADZONE;
                _controllerMovement /= CONTROLLER_MAX_INPUT * CONTROLLER_MAX_INPUT;
                
            }

            if (e.Axis == Joystick.Axis.X)
            {
                _rotation = Math.Sign(e.Position) * e.Position * e.Position - CONTROLLER_DEADZONE;
                _rotation /= CONTROLLER_MAX_INPUT * CONTROLLER_MAX_INPUT;

            }
        }

        public override Vector2f Update()
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
            return new Vector2f(currentMovement, _rotation);
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
        }

        private void _ProcessKeyboardRelease(Object sender, KeyEventArgs e)
        {
 
            if (e.Code == Keyboard.Key.W || e.Code == Keyboard.Key.Up || e.Code == Keyboard.Key.S || e.Code == Keyboard.Key.Down)
            {
                _stopwatch.Stop();
                _stopwatch.Reset();
            }
        }


    }
}
