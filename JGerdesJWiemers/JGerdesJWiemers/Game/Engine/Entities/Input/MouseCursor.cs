using JGerdesJWiemers.Game.Engine.Input;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Entities.Input
{
    class MouseCursor 
    {
        private Vector2i _position;
        private CircleShape _pointer;

        public MouseCursor()  
        {
            _pointer = new CircleShape(3);
            _position = InputManager.Instance.MousePosition;
        }
    }
}
