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
            return _points[(int)index];
        }

        public override uint GetPointCount()
        {
            return (uint)_points.Count;
        }

        public void addPoint(Vector2f point)
        {
            _points.Add(point);
            base.Update();
        }

        public void setPoint(int index, Vector2f point)
        {
            _points[index] = point;
            base.Update();
        }

        public void DeletePoint(int index)
        {
            _points.RemoveAt(index);
            base.Update();
        }
    }
}