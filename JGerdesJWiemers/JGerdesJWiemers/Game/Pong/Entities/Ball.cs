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
using JGerdesJWiemers.Game.Engine.Entities;

namespace JGerdesJWiemers.Game.Pong.Entities
{
    class Ball : CircleEntity
    {
      
        private float _generationSpeed = 5;
        private float _rotationSpeed = 2f;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Ball(float x, float y, float r, float rs) : base(x,y,r,rs)
        {
            _shape = new CircleShape(_radius);
            ((CircleShape)_shape).SetPointCount(6);
            _shape.Origin = new Vector2f(10, 10);
            _position = new Vector2f(x, y);
            _shape.FillColor = new Color(0, 0, 0, 255);
            reset();
        }

        /// <summary>
        /// 
        /// </summary>
        public void reset()
        {
            _position = new Vector2f(1280 / 2f, 720 / 2f);
            Random rand = new Random();
            _speed = new Vector2f(rand.Next(-5, 5), rand.Next(-1,1));
            //_speed = new Vector2f(1,0);
            if (System.Math.Abs(_speed.X) < 1)
            {
                _speed.X = System.Math.Sign(_speed.X);
            }
            if (_speed.X == 0)
            {
                _speed.X = -1;
            }
            //normalize
            _speed /= _speed.Length();
            //set length
            _speed *= _generationSpeed;


            if (_generationSpeed < 10)
            {
                _generationSpeed += 0.5f;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Update()
        {
            base.Update();
            if (_position.Y >= 720 || _position.Y <= 0)
            {
                _speed.Y *= -1;
                _rotationSpeed = -10f/_speed.Y;
            }

            _shape.Rotation += _rotationSpeed ;

        }

        public override void onCollision()
        {
            // todo
        }
      

    }
}
