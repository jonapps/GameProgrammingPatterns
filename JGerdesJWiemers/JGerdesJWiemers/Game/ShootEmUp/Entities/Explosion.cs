using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using JGerdesJWiemers.Game.Engine;
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
using SMath = System.Math;

namespace JGerdesJWiemers.Game.ShootEmUp.Entities
{
    class Explosion : SpriteEntity
    {

        public Explosion(World world, float x, float y, float scale = 1, float xSpeed = 0, float ySpeed = 0, float rotSpeed = 0)
            : base(world, AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_EXPLOSION1), scale)
        {

            _body.Position = new Vector2(x, y) - _body.LocalCenter;
            _body.Enabled = false;
            _sprite.SetAnimation(new Animation(0, 15, 30, false, false));
            _sprite.OnAnimationEnded += _OnAnimationEnded;
        }

        void _OnAnimationEnded(Animation animation)
        {
            _deleteMe = true;
        }

      

        internal override void PastUpdate()
        {
            base.PastUpdate();
        }

        

        public class ExplosionDef : EntityDef
        {
            public ExplosionDef(float xPos = 0, float yPos = 0, float xSpeed = 0, float ySpeed = 0, float scale = 1, float rotationSpeed = 0)
                : base(xPos, yPos, xSpeed, ySpeed, scale, rotationSpeed)
            {
            }

            public override Engine.Entity Spawn(World world)
            {
                Explosion explosion = new Explosion(world, Position.X, Position.Y, Scale, Speed.X, Speed.Y, RotationSpeed);
                return explosion;
            }
        }
    }
}
