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
            if (_position.X <= 0)
            {
                _position.X = _radius;
            }
            if (_position.Y <= 0)
            {
                _position.Y = _radius;
            }
            if (_position.X >= _window.Size.X)
            {
                _position.X = (int)_window.Size.X - _radius;
            }
            if (_position.Y >= _window.Size.Y)
            {
                _position.Y = (int)_window.Size.Y - _radius;
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
            throw new NotImplementedException();
        }

        public void PreDraw(float extra)
        {
            throw new NotImplementedException();
        }
    }
}
