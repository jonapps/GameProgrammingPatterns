using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Controller;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace JGerdesJWiemers.Game.Pong.Entities
{
    class Paddle : Entity
    {


        protected ControllerBase _controller;
        protected Rail _rail;
        protected float _railPosition = 0;
        protected float _rotation = 0;
        protected float _rotationTarget = 0;
        protected byte _alpha ;

        public Paddle(Rail rail, Color c)
        {
            _shape = new RectangleShape(new Vector2f(20, 80));
            _shape.Origin = new Vector2f(10, 40);
            _speed = new Vector2f(0, 0);
            _rail = rail;
            _shape.FillColor = c;
            _alpha = c.A;
        }

         public ControllerBase Controller
        {
            set
            {
                this._controller = value;
            }
        
         }

         public float Rotation
         {
             get
             {
                 return _rotationTarget;
             }
             set
             {
                 _rotationTarget = value;
             }
         }

        public override void Update()
        {
             if (this._controller != null)
             {
                 _railPosition += this._controller.Update() / 10;
                 _railPosition = Math.Max(-1, _railPosition);
                 _railPosition = Math.Min(1, _railPosition);
                 _rotation += (_rotationTarget - _rotation) / 10f;
             }

             Vector2f oldPosition = new Vector2f(_position.X, _position.Y);
             _position = _rail.getPointAt(_railPosition);
             _speed = _position - oldPosition;
             _shape.Rotation = _railPosition*_rail.Side*60 + _rotation;

            byte a = _shape.FillColor.A;
            float f = ( _alpha - a)/15f;
            a += (byte)f;
            _shape.FillColor = new Color(_shape.FillColor.R, _shape.FillColor.G, _shape.FillColor.B, a);
        }

        public override void Render(RenderTarget renderTarget, float extra)
        {
            renderTarget.Draw(_rail);
            base.Render(renderTarget, extra);
        }

        public void onCollision()
        {
            _shape.FillColor = new Color(_shape.FillColor.R, _shape.FillColor.G, _shape.FillColor.B, 255);
            _alpha = 80;
        }

    }
}
