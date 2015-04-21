using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Entities
{

    abstract class CircleEntity : Entity
    {
        protected float _radius;
        protected Vector2f _lastPosition;


        public float Radius
        {
            get
            {
                return _radius;
            }
        }

        public Vector2f LastPosition
        {
            get
            {
                return _lastPosition;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        public CircleEntity(float x, float y, float r) : base(x,y)
        {
            this._radius = r;
        }

        public override void Update()
        {
            _lastPosition = _GetLastPosition();
        }

        protected Vector2f _GetLastPosition()
        {
            return new Vector2f(_position.X, _position.Y);
        }
    }
}
