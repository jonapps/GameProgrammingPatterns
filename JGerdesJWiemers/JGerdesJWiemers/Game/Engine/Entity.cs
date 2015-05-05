using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.Engine.Graphics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using FarseerPhysics.Dynamics;

namespace JGerdesJWiemers.Game.Engine
{
    abstract class Entity : IUpdateable, IRenderable
    {
        protected Vector2f _position;
        protected Vector2f _speed;
        protected Shape _renderShape;
        protected Body _body;
        protected World _world;


        public Entity(float x, float y, World w)
        {
            _position = new Vector2f(x, y);
            _world = w;
        }

        public Body Body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
            }
        }


        public Shape Shape
        {
            get
            {
                return _renderShape;
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
                if (!value.Equals(null))
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
            set
            {
                _position = value;
            }
        }

        public virtual void Update()
        {

            _position += _speed;
        }

        public virtual void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            _renderShape.Position = _position + _speed * extra;
            renderTarget.Draw(_renderShape);
        }

        public abstract void onCollision();

    }
}
