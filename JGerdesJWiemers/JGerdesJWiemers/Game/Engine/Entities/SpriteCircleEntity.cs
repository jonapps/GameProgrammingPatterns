using FarseerPhysics;
using FarseerPhysics.Dynamics;
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
    class SpriteCircleEntity : CircleEntity
    {
        protected AnimatedSprite _sprite;
        protected RenderStates _renderStates;

        public SpriteCircleEntity(Texture tex, int spriteWidth, int spriteHeight, float x, float y, float radius, World w, BodyType bodyType = BodyType.Dynamic) : base(x,y,radius, w, bodyType)
        {
            _sprite = new AnimatedSprite(tex, spriteWidth, spriteHeight);
            float scale = ConvertUnits.ToDisplayUnits(radius) * 2 / spriteWidth;
            _sprite.Origin = new Vector2f(spriteWidth/2f, spriteHeight / 2f);
            _sprite.Scale = new Vector2f(ConvertUnits.ToSimUnits(scale), ConvertUnits.ToSimUnits(scale));
            
            _renderStates = new RenderStates(BlendMode.Alpha);
        }

        public override void Render(RenderTarget renderTarget, float extra)
        {
            if (_body != null)
            {
                System.Console.WriteLine(_ConvertVectorToVector2f(_body.LinearVelocity));
                _sprite.Position = _ConvertVectorToVector2f(_body.Position) + _ConvertVectorToVector2f(_body.LinearVelocity) * extra;
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
