using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace JGerdesJWiemers.Game.Pong.Entities
{
    class Rail
    {
        public static readonly int SIDE_LEFT = -1;
        public static readonly int SIDE_RIGHT = 1;

        float _radius = 360;
        Vector2f center;
        int _side;

        public Rail(int side)
        {
            center = new Vector2f(1280/2f + side*200, 720 / 2f);
            _side = side;
        }

        public Vector2f getPointAt(float index)
        {
            index *= _side;
            Vector2f pos =  new Vector2f((float)Math.Cos(index), (float)Math.Sin(index));
            pos *= _side *_radius;
            pos += center;
            return pos;
        }
    }
}
