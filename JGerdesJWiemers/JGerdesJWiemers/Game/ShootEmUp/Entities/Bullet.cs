using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Entities;
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
    class Bullet : SpriteEntity
    {

        private float _speed = 10000000000f;
        private Vector2 _direction;

        public Bullet(float x, float y, World w, Vector2 direction)
            : base(AssetLoader.Instance.LoadTexture(AssetLoader.TEXTURE_SPACESHIP), 32, 48)
        {
            _sprite.Origin = new Vector2f(16, 24);
            _sprite.Scale = new Vector2f(ConvertUnits.ToSimUnits(0.5), ConvertUnits.ToSimUnits(0.5));
            Vertices bl = new Vertices();
            bl.Add(new Vector2(0, -1.4f));
            bl.Add(new Vector2(-0.35f, 0.5f));
            bl.Add(new Vector2(-1, 0.9f));
            bl.Add(new Vector2(-1, 1.2f));
            bl.Add(new Vector2(0, 1.3f));
            bl.Add(new Vector2(1, 1.2f));
            bl.Add(new Vector2(1, 0.9f));
            bl.Add(new Vector2(0.35f, 0.5f));

            _Create(x, y, bl, w);
            _body.IsBullet = true;
            _direction = direction;
            _body.ApplyForce(direction * _speed);
        }


        public override void Update()
        {
            base.Update();
        }
    }
}
