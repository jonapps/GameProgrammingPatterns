using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.Engine.Graphics;
using SFML.Graphics;
using SFML.Window;
using JGerdesJWiemers.Game.Engine.Controller;
using SFML.System;

namespace JGerdesJWiemers.Game.Engine
{
    abstract class Entity : IUpdateable, IRenderable
    {
        protected Vector2f _position;
        protected Vector2f _speed;
        protected Shape _shape;

        public Entity(float x, float y)
        {
            _position = new Vector2f(x, y);
        }

        public Shape Shape
        {
            get
            {
                return _shape;
            }
        }
        
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

        public Vector2f Position
        {
            get
            {
                return _position;
            }
        }

        public virtual void Update()
        {
            _position += _speed;
        }

        public virtual void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            _shape.Position = _position + _speed * extra;
            renderTarget.Draw(_shape);
        }

        public abstract void onCollision();
    }
}
