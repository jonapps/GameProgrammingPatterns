using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.Engine.Graphics;
using SFML.Graphics;
using SFML.Window;
using JGerdesJWiemers.Game.Engine.Controller;

namespace JGerdesJWiemers.Game.Engine
{
    abstract class Entity : IUpdateable, IRenderable
    {
        protected Vector2f _position;
        protected Vector2f _speed;
        protected Shape _shape;
        protected ControllerBase _controller;

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
        public ControllerBase Controller
        {
            set
            {
                this._controller = value;
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
            if (this._controller != null)
            {
                this._position.Y += this._controller.Update() * Game.PADDLE_GAME_SPEED;
            }
            _position += _speed;
        }

        public virtual void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            _shape.Position = _position;
            renderTarget.Draw(_shape);
        }

        public FloatRect GetBoundingBox()
        {
            _shape.Position = _position - _shape.Origin;
            return _shape.GetGlobalBounds();
        }
    }
}
