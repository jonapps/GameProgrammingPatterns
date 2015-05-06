
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Entities
{

    class CircleEntity : Entity
    {
        protected float _rotationSpeed;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r"></param>
        /// <param name="w"></param>
        public CircleEntity(float x, float y, float r, World w, BodyType bodyType = BodyType.Dynamic) : base()
        {
            _rotationSpeed = 0f;
            _body = BodyFactory.CreateCircle(w, r, 1f, new Vector2(x, y), bodyType, this);
            _body.Position = new Vector2(x, y);
            _renderShape = new CircleShape(r);
            _renderShape.Origin = new Vector2f(r, r);
        }

        public override void Update()
        {
            base.Update();

        }
    }
}
