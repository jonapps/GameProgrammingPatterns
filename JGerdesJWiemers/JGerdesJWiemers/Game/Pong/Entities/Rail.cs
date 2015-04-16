using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace JGerdesJWiemers.Game.Pong.Entities
{
    class Rail : Shape
    {
        public static readonly int SIDE_LEFT = -1;
        public static readonly int SIDE_RIGHT = 1;

        float _radius = 360;
        int _polyCount = 60;
        Vector2f center;
        int _side;

        public int Side
        {
            get
            {
                return _side;
            }
        }

        public Rail(int side): base()
        {
            center = new Vector2f(1280/2f + side*200, 720 / 2f);
            _side = side;
            base.Update();
            FillColor = new Color(0, 0, 0, 0);
            OutlineColor = new Color(255, 255, 255, 128);
            OutlineThickness = 2f;
           
        }

        public Vector2f getPointAt(float index)
        {
            index *= _side;
            Vector2f pos =  new Vector2f((float)Math.Cos(index), (float)Math.Sin(index));
            pos *= _side *_radius;
            pos += center;
            return pos;
        }

        public override Vector2f GetPoint(uint index)
        {
            float railPosition = (index / (float)_polyCount) * 2 - 1;
            return getPointAt(railPosition);
        }

        public override uint GetPointCount()
        {
            return (uint)_polyCount;
        }
    }
}
