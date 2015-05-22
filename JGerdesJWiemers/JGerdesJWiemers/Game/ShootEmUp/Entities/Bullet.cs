using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Common.PhysicsLogic;
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

        public Bullet(float x, float y, World w, Vector2 direction, float rotation)
            : base(w, AssetLoader.Instance.LoadTexture(AssetLoader.TEXTURE_SPACESHIP), 0.5f, x, y)
        {
            _body.IsBullet = true;
            _direction = direction;
            _body.ApplyLinearImpulse(direction * _speed);
            _body.Rotation = rotation;
            _body.CollisionCategories = EntityCategory.Bullet;

        }

        public override void Update()
        {
            base.Update();
        }
    }
}
