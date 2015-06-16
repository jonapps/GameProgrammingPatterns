using JGerdesJWiemers.Game.Engine.Input;
using JGerdesJWiemers.Game.Engine.Interfaces;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Entities.Input
{
    class MouseCursor : IDrawable
    {
        private Vector2i _position;
        private CircleShape _pointer;
        private Window _window;
        private int _radius = 3;
        private float _scrollSpeed = 10;

        public MouseCursor(Window w)  
        {
            _window = w;
            _pointer = new CircleShape(_radius);
            _position = InputManager.Instance.MousePosition;
            _pointer.Origin = new Vector2f(_radius,_radius);
            
        }

        public void Update()
        {
            _position = InputManager.Instance.MousePosition;
            
            float left, top, right, bottom;
            left = 0;
            top = 0;
            right = _window.Size.X;
            bottom = _window.Size.Y;

            if (_position.X <= left + _radius)
            {
                _position.X = (int)left + _radius;
            }
            if (_position.Y <= top + _radius)
            {
                _position.Y = (int)top + _radius;

            }
            if (_position.X >= right - _radius)
            {
                _position.X = (int)right - _radius;

            }
            if (_position.Y >= bottom - _radius)
            {
                _position.Y = (int)bottom - _radius;
            }

            InputManager.Instance.MousePosition = _position;
            
        }

        

        public void Draw(RenderTarget target, RenderStates states)
        {
            _pointer.Position = _ConvertVector2iToVector2f(_position);
            target.Draw(_pointer);
        }

        private Vector2f _ConvertVector2iToVector2f(Vector2i v)
        {
            return new Vector2f(v.X, v.Y);
        }


        public void PastUpdate()
        {
            //throw new NotImplementedException();
        }

        public void PreDraw(float extra)
        {
            
        }
    }
}
