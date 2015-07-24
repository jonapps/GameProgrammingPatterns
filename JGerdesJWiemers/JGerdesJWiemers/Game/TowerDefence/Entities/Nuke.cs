using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.EventSystem.Events;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.TowerDefence.Screens;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMath = System.Math;

namespace JGerdesJWiemers.Game.TowerDefence.Entities
{
    class Nuke : SpriteEntity
    {
        public static readonly String EVENT_SPAWN = "nuke.spawn";
        private long _timeToLive = 10000;
        private long _startTime = 0;

        private Vector2 _destination;

        private Enemy _toDealDmg;

        private int _damage = 0;

        public class Def
        {
            public Vector2 Position { get; set; }
            public Vector2 Destination { get; set; }
            public float Speed { get; set; }
            public int Damage { get; set; }
            public Color Color { get; set; }
        }


        public Nuke(World world, Def def)
            :base(world, AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_BULLET))
        {
            _sprite.Scale = new Vector2f(0.2f, 0.2f);
            _sprite.Color = def.Color;
            _body.Position = def.Position;
            _body.FixedRotation = true;
            _body.CollisionCategories = EntityCategory.Nuke;
            _body.CollidesWith = EntityCategory.Monster;
            _damage = def.Damage;
            Vector2 direction = (def.Destination - _body.WorldCenter);
            direction.Normalize();
            _body.LinearVelocity = direction * def.Speed;
            _sprite.Rotation =  (float) (SMath.Atan2(direction.Y, direction.X) * (180 / SMath.PI));
            _startTime = Game.ElapsedTime;
            _body.OnCollision += _OnCollision;

            _destination = def.Destination;
        }

        private bool _OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            Enemy m = null;
            if ((m = fixtureB.Body.UserData as Enemy) != null)
            {
                if (!_deleteMe)
                {
                    _toDealDmg = m;
                }
                _deleteMe = true;   
            }
            return true;
        }

        public override void PastUpdate()
        {
            base.PastUpdate();
            if (_deleteMe && _toDealDmg != null)
            {
                _toDealDmg.ApplyDamage(_damage);
            }
        }

        public override void Update()
        {
            base.Update();
            if ((Game.ElapsedTime - _startTime) > _timeToLive)
            {
                _deleteMe = true;
            }
        }

        public override void PreDraw(float extra)
        {
            base.PreDraw(extra);
            if (_sprite.Scale.X < 1)
            {
                _sprite.Scale *= 1.1f;
            }
        }
 
    }
}
