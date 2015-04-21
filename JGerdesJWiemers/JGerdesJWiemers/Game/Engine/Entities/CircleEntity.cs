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
        protected float _rotationSpeed;

        public float Radius
        {
            get
            {
                return _radius;
            }
            set
            {
                _radius = value;
            }
        }

        public float RotationSpeed
        {
            get
            {
                return _rotationSpeed;
            }
            set
            {
                _rotationSpeed = value;
            }
        }

        public Vector2f LastPosition
        {
            get
            {
                return _lastPosition;
            }
            set
            {
                _lastPosition = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        public CircleEntity(float x, float y, float r, float rs) : base(x,y)
        {
            _radius = r;
            _rotationSpeed = rs;
        }

        public override void Update()
        {
            base.Update();
            _lastPosition = _GetLastPosition();
        }

        protected Vector2f _GetLastPosition()
        {
            return new Vector2f(_position.X, _position.Y);
        }
    }
}
