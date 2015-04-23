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
using JGerdesJWiemers.Game.Engine.Input;

namespace JGerdesJWiemers.Game.Pong.Controller
{
    class Player : ControllerBase
    {
        float _speed;

        public Player(Paddle p, int playerId) : base(p)
        {
            InputManager.Channel[playerId].OnUp += delegate(float value)
            {
                _speed = -(value*value);
            };
            InputManager.Channel[playerId].OnDown += delegate(float value)
            {
                _speed = value*value;
            };

            InputManager.Channel[playerId].OnAction3 += delegate(bool pressed)
            {
                p.Rotation = pressed ? -60 : 0;
            };

            InputManager.Channel[playerId].OnAction4 += delegate(bool pressed)
            {
                p.Rotation = pressed ? 60 : 0;
            };
        }
 


        public override float Update()
        {
            return _speed;
        }


    }
}
