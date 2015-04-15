using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Utils;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace JGerdesJWiemers.Game.Pong.Entities
{
    class Ball : Entity
    {
       
        private float _radius = 10;

        public Ball(float x, float y)
        {
            _shape = new CircleShape(_radius);
            ((CircleShape)_shape).SetPointCount(6);
            _shape.Origin = new Vector2f(5, 5);
            _position = new Vector2f(x, y);
            _speed = new Vector2f(10, 0);
        }


        public override void Update()
        {
            base.Update();
            if (_position.Y >= 720 || _position.Y <= 0)
            {
                _speed.Y *= -1;
            }


            if(_position.X >= 1280){
                _speed.X *= -1;
            }
        }

        public bool CollideWith(Paddle paddle)
        {

            return false;
        }
      

    }
}
