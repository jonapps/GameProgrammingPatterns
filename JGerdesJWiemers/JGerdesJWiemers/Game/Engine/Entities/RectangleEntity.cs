using FarseerPhysics.Dynamics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Entities
{
    abstract class RectangleEntity : Entity
    {

        protected List<Vector2f> _lastPoints;


        public List<Vector2f> LastPoints
        {
            get
            {
                if (_lastPoints.Count < 4)
                {
                    return GetCurrentPoints();
                }
                return _lastPoints;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public RectangleEntity(float x, float y, World w) : base(x, y, w)
        {
            _lastPoints = new List<Vector2f>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Vector2f> GetCurrentPoints()
        {
            List<Vector2f> result = new List<Vector2f>();
            for (uint i = 0; i < _renderShape.GetPointCount(); ++i)
            {
                Vector2f point = _renderShape.Transform.TransformPoint(_renderShape.GetPoint(i));
                result.Add(new Vector2f(point.X, point.Y));
            }
            return result;
        }

        public override void Update()
        {
            _lastPoints = GetCurrentPoints();
            base.Update();
        }
    }
}
