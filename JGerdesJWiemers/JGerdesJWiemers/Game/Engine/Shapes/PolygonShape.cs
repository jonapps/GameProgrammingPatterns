using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Shapes
{
    class PolygonShape : Shape
    {

        private List<Vector2f> _points;

        public PolygonShape(List<Vector2f> points)
            : base()
        {
            _points = points;
            base.Update();
        }

        public override Vector2f GetPoint(uint index)
        {
            System.Console.WriteLine("=!=asdasidgiu21 39hodhoiahsdoiahsd");
            return _points[(int)index];
        }

        public override uint GetPointCount()
        {
            return (uint)_points.Count;
        }
    }
}