using JGerdesJWiemers.Game.Engine.Graphics;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Entities
{
    class SpriteEntity : PolygonEntity
    {
        AnimatedSprite _sprite;
        RenderStates _renderStates;

        public SpriteEntity(Texture tex, int width, int height) : base()
        {
            _sprite = new AnimatedSprite(tex, width, height);
            _renderStates = new RenderStates();
            _renderStates.BlendMode = BlendMode.Alpha;
        }

        public override void Render(RenderTarget renderTarget, float extra)
        {
            _sprite.Draw(renderTarget, _renderStates);
            //base.Render(renderTarget, extra);  //leave for debugging
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
