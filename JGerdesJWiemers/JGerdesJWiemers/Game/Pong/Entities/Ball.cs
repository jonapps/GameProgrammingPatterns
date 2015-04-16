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
        private float _generationSpeed = 5;

        public Ball(float x, float y)
        {
            _shape = new CircleShape(_radius);
            ((CircleShape)_shape).SetPointCount(6);
            _shape.Origin = new Vector2f(5, 5);
            _position = new Vector2f(x, y);
            _speed = new Vector2f(5f,-1.4f);
        }

        public void reset()
        {
            _position = new Vector2f(1280 / 2f, 720 / 2f);
            Random rand = new Random();
            _speed = new Vector2f(rand.Next(-5,5), rand.Next(-5,5));
            //normalize
            _speed /= _speed.Length();
            //set length
            _speed *= _generationSpeed;
            _generationSpeed += 2;
        }

        public override void Update()
        {
            base.Update();
            if (_position.Y >= 720 || _position.Y <= 0)
            {
                _speed.Y *= -1;
            }

        }

        public bool CollideWith(Paddle paddle, RenderTarget w)
        {
            Shape shape = paddle.Shape;
            
            Vector2f p1;
            Vector2f p2;
            Vector2f normal;
            Vector2f tangente;
            Vector2f potencial;
            
            uint j;
            for (uint i = 0; i < shape.GetPointCount(); ++i)
            {
                if (i == shape.GetPointCount() - 1)
                {
                    j = 0;
                }
                else
                {
                    j = i + 1;
                }
                
                p1 = shape.GetPoint(i);
                p1 = shape.Transform.TransformPoint(p1);
                p2 = shape.GetPoint(j);
                p2 = shape.Transform.TransformPoint(p2);
                tangente = p2 - p1;
                tangente /= tangente.Length();
                normal = new Vector2f(tangente.Y, tangente.X * -1) * -1;
                potencial = _position + normal * _radius;
                float x = (potencial - p1).Length();
                float y = (potencial - p2).Length();
                float z = (p1 - p2).Length();
                if (x + y - z < 0.4f)
                {
                    float speedLength = _speed.Length();
                    _position -= _speed;
                    _speed = JGerdesJWiemers.Game.Engine.Utils.Math.Scalar(tangente, _speed) * tangente - JGerdesJWiemers.Game.Engine.Utils.Math.Scalar(normal, _speed) * normal;
                    
                    return true;
                }
            }
                return false;
        }
      

    }
}
