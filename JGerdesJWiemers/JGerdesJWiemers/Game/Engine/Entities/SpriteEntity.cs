using FarseerPhysics;
using JGerdesJWiemers.Game.Engine.Graphics;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Entities
{
    class SpriteEntity : PolygonEntity
    {
        protected AnimatedSprite _sprite;
        protected RenderStates _renderStates;

        public SpriteEntity(Texture tex, int width, int height) : base()
        {
            _sprite = new AnimatedSprite(tex, width, height);
            _sprite.Scale = new Vector2f(ConvertUnits.ToSimUnits(1), ConvertUnits.ToSimUnits(1));
            _renderStates = new RenderStates(BlendMode.Alpha);
        }

        public override void Render(RenderTarget renderTarget, float extra)
        {
            if (_body != null)
            {
                _sprite.Position = _ConvertVectorToVector2f(_body.Position);
                _sprite.Rotation = _body.Rotation * 180 / (float) Math.PI;
            }
            _sprite.Draw(renderTarget, _renderStates);
            //base.Render(renderTarget, extra);  //leave for debugging
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
