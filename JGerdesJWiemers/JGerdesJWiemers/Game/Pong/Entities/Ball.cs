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
       

        public Ball(float x, float y)
        {
            _shape = new CircleShape(10);
            ((CircleShape)_shape).SetPointCount(6);
            _shape.Origin = new Vector2f(5, 5);
            _position = new Vector2f(x, y);
            _speed = new Vector2f(0, 0);
        }

    }
}
