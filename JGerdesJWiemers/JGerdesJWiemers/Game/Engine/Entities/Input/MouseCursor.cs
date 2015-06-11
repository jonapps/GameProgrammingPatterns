using JGerdesJWiemers.Game.Engine.Input;
using JGerdesJWiemers.Game.Engine.Interfaces;
using SFML.Graphics;
using SFML.System;
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

        public MouseCursor()  
        {
            _pointer = new CircleShape(3);
            _position = InputManager.Instance.MousePosition;
        }

        public void Update()
        {
            _position = InputManager.Instance.MousePosition;
        }

        public void PastUpdate()
        {
           
        }

        public void PreRender(float extra)
        {
            
        }

        public void Render()
        {
            
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            
        }
    }
}
