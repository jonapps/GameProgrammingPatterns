using FarseerPhysics;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Graphics;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Entities
{
    class ScrollingBackground : SpriteEntity
    {

        protected Sprite _sprite2;
        protected Vector2f _speed;

        public ScrollingBackground(Texture tex, int width, int height, float x, float y, float speedX, float speedY)
            : base(tex, width, height)
        {
            _sprite2 = new AnimatedSprite(tex, width, height);
            _sprite2.Scale = new Vector2f(ConvertUnits.ToSimUnits(1), ConvertUnits.ToSimUnits(1));

            _speed = new Vector2f(speedX, speedY);

            float scale = 1;
            if (height < 720)
            {
                scale = 720 / (float) height;
            }
            if (width < 1280)
            {
                scale = Math.Max(scale, 1280 / (float) width);
            }

            _sprite.Scale *= scale;
            _sprite2.Scale *= scale;

            _sprite.Position = new Vector2f(x, y);
            _sprite2.Position = _sprite.Position + new Vector2f(width * Math.Sign(speedX*-1), height * Math.Sign(speedY*-1));
        }

        public override void Render(RenderTarget renderTarget, float extra)
        {
            _sprite.Draw(renderTarget, _renderStates);
            _sprite2.Draw(renderTarget, _renderStates);
        }

        public override void Update()
        {
            _sprite.Position = _sprite.Position + _speed;
            _sprite2.Position = _sprite.Position + _speed;
        }
    }
}
