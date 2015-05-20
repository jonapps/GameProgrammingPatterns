using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Entities
{
    class ShapeEntity: Entity
    {
        protected Shape _renderShape;

        public ShapeEntity(Shape shape)
        {
            _renderShape = shape;
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
 	        _renderShape.Position = _ConvertVectorToVector2f(_body.Position) + _ConvertVectorToVector2f(_body.LinearVelocity) * extra;
            _renderShape.Rotation = _body.Rotation * 180 / (float)Math.PI;
            renderTarget.Draw(_renderShape);
        }
    }
}
