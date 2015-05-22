using FarseerPhysics;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Utils;
using SFML.Graphics;
using SFML.System;
using SMath = System.Math;
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
        protected Vector2f _start1;
        protected Vector2f _start2;
        protected Vector2f _restart;

        public ScrollingBackground(TextureContainer tex, float x, float y, float speedX, float speedY)
            : base(tex)
        {
            float width = tex.Width,
                  height = tex.Height;

            _sprite2 = new AnimatedSprite(tex.Texture, (int)width, (int)height);
            _sprite2.Scale = new Vector2f(ConvertUnits.ToSimUnits(1), ConvertUnits.ToSimUnits(1));

            _speed = new Vector2f(speedX, speedY);
            
            //scale to fit screen
            float scale = 1;
            if (height < 720)
            {
                scale = 720 / height;
            }
            if (width < 1280)
            {
                scale = SMath.Max(scale, 1280 / width);
            }

            //setup positions
            _start1 = new Vector2f(x, y);
            _start2 = _start1 + new Vector2f(
                        ConvertUnits.ToSimUnits(width * SMath.Sign(speedX * -1)), 
                        ConvertUnits.ToSimUnits(height * SMath.Sign(speedY * -1))
            );
            _restart = _start1 + new Vector2f(
                        ConvertUnits.ToSimUnits(width * SMath.Sign(speedX)),
                        ConvertUnits.ToSimUnits(height * SMath.Sign(speedY))
            );

            _sprite2.Position = _start1;
            _sprite.Position = _start2;


            _sprite.Scale *= scale;
            _sprite2.Scale *= scale;

            
        }

        public override void Render(RenderTarget renderTarget, float extra)
        {
            _sprite.Draw(renderTarget, _renderStates);
            _sprite2.Draw(renderTarget, _renderStates);
        }

        public override void Update()
        {
            _sprite.Position = _sprite.Position + _speed;
            _sprite2.Position = _sprite2.Position + _speed;

            //TODO: make this work for other diretory
            if (_sprite.Position.X < _restart.X || _sprite.Position.Y < _restart.Y)
            {
                _sprite.Position = _start2;
            }

            if (_sprite2.Position.X < _restart.X || _sprite2.Position.Y < _restart.Y)
            {
                _sprite2.Position = _start2;
            }

        }

        public class BackgroundDef : EntityDef
        {
            string TextureContainerName;
            public BackgroundDef() : base() { }

            public override Engine.Entity Spawn(World world)
            {
                ScrollingBackground bg = new ScrollingBackground(AssetLoader.Instance.getTexture(TextureContainerName), Position.X, Position.Y, Speed.X, Speed.Y);
                return bg;
            }
        }
    }
}
