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
    class Paddle : Entity
    {

        public Paddle()
        {
            _shape = new RectangleShape(new Vector2f(20, 80));
            _shape.Origin = new Vector2f(10, 40);
            _position = new Vector2f(100, 100);
            _speed = new Vector2f(0, 0);
        }

    }
}
