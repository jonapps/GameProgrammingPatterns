using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.Engine.Graphics;
using SFML.Graphics;
using SFML.Window;

namespace JGerdesJWiemers.Game.Engine
{
    abstract class Entity : IUpdateable, IRenderable
    {
        protected Vector2f _position;
        protected Vector2f _speed;
        protected Shape _shape;

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

        public void Update()
        {
            _position += _speed;
        }

        public void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            _shape.Position = _position + extra * _speed;
            renderTarget.Draw(_shape);
        }
    }
}
