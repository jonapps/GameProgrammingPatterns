using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Utils;
using SFML.Window;
using SFML.Graphics;

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
            _speed = new Vector2f(4, 10);
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
            FloatRect box = paddle.GetBoundingBox();

            Vector2f distance = new Vector2f(System.Math.Abs(box.Left - _position.X), System.Math.Abs(box.Top - _position.Y));
            Vector2f halfSize = new Vector2f(box.Width / 2f, box.Height / 2f);

            if (distance.X > halfSize.X + _radius || distance.Y >halfSize.Y + _radius)
            {
                return false;
            }

            if (distance.X <= halfSize.X || distance.Y <= halfSize.Y || distance.Distance2To(halfSize) <= _radius * _radius)
            {
                _position -= _speed;
                _speed.X *= -1;
                return true;
            } 

            return false;
        }
      

    }
}
