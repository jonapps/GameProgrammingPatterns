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

        private float _speed = 10000000f;
        private Vector2 _direction;

        public Bullet(float x, float y, World w, Vector2 direction)
            : base(AssetLoader.Instance.LoadTexture(AssetLoader.TEXTURE_SPACESHIP), 32, 48)
        {
            _sprite.Origin = new Vector2f(16, 24);

            Vertices bl = new Vertices();
            bl.Add(new Vector2(0, -2.8f));
            bl.Add(new Vector2(-0.7f, 1));
            bl.Add(new Vector2(-2, 1.8f));
            bl.Add(new Vector2(-2, 2.4f));
            bl.Add(new Vector2(0, 2.6f));
            bl.Add(new Vector2(2, 2.4f));
            bl.Add(new Vector2(2, 1.8f));
            bl.Add(new Vector2(0.7f, 1));

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
