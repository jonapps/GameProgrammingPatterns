using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.Engine;
using SFML.Window;
using SFML.Graphics;

namespace JGerdesJWiemers.Game.Pong.Entities
{
    class Ball : Entity
    {
        private CircleShape _shape;

        private Vector2f _position;
        private Vector2f _speed;

        public Vector2f Speed
        {
            get
            {
                return _speed;
            }

            set
            {
                if (value.Equals(null))
                {
                    _speed = value;
                }
            }
        }

        public Ball(float x, float y)
        {
            _shape = new CircleShape(10);
            _shape.SetPointCount(6);
            _shape.Origin = new Vector2f(5, 5);
            _position = new Vector2f(x, y);

            _speed = new Vector2f(0, 0);
        }

        public override void Update()
        {
            _shape.Position += _speed;
        }

        public override void Render(RenderTarget renderTarget, float extra)
        {
            _shape.Position = _position + extra * _speed;
            renderTarget.Draw(_shape);
        }
    }
}
