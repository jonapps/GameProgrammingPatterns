using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Common;
using FarseerPhysics.Common.PhysicsLogic;
using FarseerPhysics.Dynamics;
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

        protected AABB _aabb;

        //debug rect
        protected RectangleShape _debugrect; 


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

        private bool _OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            if (fixtureA.Body.UserData is SpaceShip || fixtureB.Body.UserData is SpaceShip || fixtureA.Body.UserData is Bullet && fixtureB.Body.UserData is Bullet)
            {
                return false;
            }

            _deleteMe = true;
            return true;
        }

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
        }


        private void _ApplyBlastImpulse(Body body, Vector2 blastCenter)
        {
            Vector2 impulse = new Vector2();
            impulse.X = blastCenter.X - body.WorldCenter.X ;
            impulse.Y = blastCenter.Y - body.WorldCenter.Y;
            impulse *= (_blastStrength - body.WorldCenter.Length()) * -1;
            body.ApplyForce(impulse);
        }

        public override void Update()
        {
            if (_deleteMe)
            {
                _Blast();
            }
            base.Update();
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            base.Render(renderTarget, extra);


            if (_debug)
            {
                _debugrect.Position = _ConvertVectorToVector2f(_body.WorldCenter);
                renderTarget.Draw(_debugrect);
            }
            
        }
    }
}
