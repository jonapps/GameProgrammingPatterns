using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Common;
using FarseerPhysics.Common.PhysicsLogic;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Utils;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Entities
{
    abstract class Bullet : SpriteEntity
    {
        protected float _speed;
        protected Vector2 _direction;
        protected World _world;
        protected float _blastRadius;
        protected float _blastStrength;
        protected bool _blow = false;
        protected AABB _aabb;

        protected float _timeToLive = 1000;

        //debug rect
        protected RectangleShape _debugrect; 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="texture"></param>
        /// <param name="direction"></param>
        /// <param name="rotation"></param>
        /// <param name="scale"></param>
        /// <param name="speed"></param>
        public Bullet(float x, float y, World w, TextureContainer texture, Vector2 direction, float rotation, float scale, float speed)
            : base(w, texture, 0.5f, x, y)
        {
            _body.IsBullet = true;
            _direction = direction;
            _body.ApplyLinearImpulse(direction * speed);
            _body.Rotation = rotation;
            _body.CollisionCategories = EntityCategory.Bullet;
            _body.OnCollision += _OnCollision;
            _world = w;
            _aabb = new AABB();
            // debug rect
            _debugrect = new RectangleShape(new Vector2f(ConvertUnits.ToSimUnits(texture.Width), ConvertUnits.ToSimUnits(texture.Height)));
            _debugrect.Origin = new Vector2f(ConvertUnits.ToSimUnits(texture.Width / 2), ConvertUnits.ToSimUnits(texture.Height / 2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fixtureA"></param>
        /// <param name="fixtureB"></param>
        /// <param name="contact"></param>
        /// <returns></returns>
        private bool _OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            if (fixtureA.Body.UserData is SpaceShip || fixtureB.Body.UserData is SpaceShip || fixtureA.Body.UserData is Bullet && fixtureB.Body.UserData is Bullet)
            {
                return false;
            }
            _blow = true;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void _Blast()
        {
            _aabb.LowerBound = _body.WorldCenter - new Vector2(_blastRadius,_blastRadius);
            _aabb.UpperBound = _body.WorldCenter + new Vector2(_blastRadius,_blastRadius);
            List<Fixture> bodies = _world.QueryAABB(ref _aabb);
            int bodyCount = bodies.Count;
            for (int i = 0; i < bodyCount; ++i)
            {
                _ApplyBlastImpulse(bodies[i].Body, _body.WorldCenter);
            }
            _deleteMe = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="blastCenter"></param>
        private void _ApplyBlastImpulse(Body body, Vector2 blastCenter)
        {
            Vector2 impulse = new Vector2();
            impulse.X = blastCenter.X - body.WorldCenter.X ;
            impulse.Y = blastCenter.Y - body.WorldCenter.Y;
            impulse *= (_blastStrength - body.WorldCenter.Length()) * -1;

            Entity e = body.UserData as Entity;
            e.ApplyDamage((int)_blastStrength);
            body.ApplyForce(impulse);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Update()
        {
            _timeToLive -= Game.ElapsedFrameTime;
            base.Update();
        }

        internal override void PastUpdate()
        {
            base.PastUpdate();
            if (_blow || (_timeToLive <= 0))
            {
                _Blast();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="renderTarget"></param>
        /// <param name="extra"></param>
        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            base.Render(renderTarget, extra);
            if (_debug)
            {
                _debugrect.Position = _ConvertVectorToVector2f(_body.WorldCenter);
                renderTarget.Draw(_debugrect);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dmg"></param>
        public override void ApplyDamage(int dmg)
        {
            base.ApplyDamage(dmg);
            if (_health <= 0)
            {
                _blow = true;
            }
        }
    }
}
