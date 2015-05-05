using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
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
        public CircleEntity(float x, float y, float r, World w) : base(x,y,w)
        {
            _radius = r;
            _rotationSpeed = 0f;
            _body = BodyFactory.CreateCircle(_world, r, 1f);
            _body.Position = new Vector2(x, y);
        }

        public override void Update()
        {
            _lastPosition = GetCurrentPosition();
            base.Update();

        }

        public  Vector2f GetCurrentPosition()
        {
            return new Vector2f(_position.X, _position.Y);
        }
    }
}
