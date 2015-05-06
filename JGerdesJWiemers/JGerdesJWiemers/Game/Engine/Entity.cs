using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.Engine.Graphics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace JGerdesJWiemers.Game.Engine
{
    abstract class Entity : IUpdateable, IRenderable
    {

        protected Shape _renderShape;
        protected Vector2f _renderPosition;
        protected Body _body;


        public Entity()
        {
        }

        public Body Body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
            }
        }


        public Shape RenderShape
        {
            get
            {
                return _renderShape;
            }
        }
        

        public virtual void Update()
        {

        }

        public virtual void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            _renderShape.Position = _ConvertVectorToVector2f(_body.Position + _body.LinearVelocity * extra);
            _renderShape.Rotation = _body.Rotation * 180 / (float)Math.PI;
            renderTarget.Draw(_renderShape);
        }


        protected Vector2f _ConvertVectorToVector2f(Vector2 vec)
        {
            return new Vector2f(vec.X, vec.Y);
        }
    }
}
