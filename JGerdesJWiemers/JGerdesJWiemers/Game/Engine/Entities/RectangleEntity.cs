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
                return _lastPoints;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public RectangleEntity(float x, float y) : base(x, y)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Vector2f> GetCurrentPoints()
        {
            List<Vector2f> result = new List<Vector2f>();
            for (uint i = 0; i < _shape.GetPointCount(); ++i)
            {
                result.Add(_shape.Transform.TransformPoint(_shape.GetPoint(i)));
            }
            return result;
        }

        public override void Update()
        {
            base.Update();
            _lastPoints = GetCurrentPoints();
        }
    }
}
