using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Utils;
using Microsoft.Xna.Framework;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Entities
{
    class Astronaut: SpriteEntity
    {

        private Animation _rotateAnimation;

        public Astronaut(Vector2f position, World world, float scale = 1)
            : base(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_ASTRONAUT), 64, 83)
        {
            Vertices vertices = new Vertices();
            //TODO: fit to sprite
            float w = ConvertUnits.ToSimUnits(scale * 64);
            float h = ConvertUnits.ToSimUnits(scale * 83);
            vertices.Add(new Vector2(0, 0));
            vertices.Add(new Vector2(w, 0));
            vertices.Add(new Vector2(w, h));
            vertices.Add(new Vector2(0, h));
            _Create(position.X, position.Y, vertices, world);

            _sprite.Scale = new Vector2f(_sprite.Scale.X * scale, _sprite.Scale.Y * scale);

            _rotateAnimation = new Animation(0, 70, 20, true, false);
            _sprite.SetAnimation(_rotateAnimation);

            //_body.ApplyLinearImpulse(new Vector2(10f, 1f));
            _body.LinearVelocity = new Vector2(2f, 3f);
            _body.AngularVelocity = 0.2f;
        }
    }
}
