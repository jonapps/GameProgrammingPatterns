using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Input;
using JGerdesJWiemers.Game.Engine.Utils;
using Microsoft.Xna.Framework;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMath = System.Math;
using GameScreen = JGerdesJWiemers.Game.ShootEmUp.Screens.Game;
using JGerdesJWiemers.Game.Engine.Graphics.Screens.Interfaces;
using JGerdesJWiemers.Game.ShootEmUp.Weapons;
using JGerdesJWiemers.Game.Engine;

namespace JGerdesJWiemers.Game.ShootEmUp.Entities
{
    class SpaceShip : SpriteEntity
    {

        private static readonly float SPEED_DEFAULT = 40;
        private Weapon _currentWeapon;
        private float _currentSpeed = SPEED_DEFAULT;
        private EntityHolder _eHolder;
        private bool _spawnBullets = false;
        private World _world;


        public SpaceShip(float x, float y , World w, EntityHolder gscreen)
            : base(AssetLoader.Instance.LoadTexture(AssetLoader.TEXTURE_SPACESHIP), 32, 48)
        {
            _currentWeapon = new RocketLauncher();

            _sprite.Origin = new Vector2f(16, 24);
            _eHolder = gscreen;
            _world = w;
            Vertices sp = new Vertices();

            sp.Add(new Vector2(0, -2.8f));
            sp.Add(new Vector2(-0.7f, 1));
            sp.Add(new Vector2(-2, 1.8f));
            sp.Add(new Vector2(-2, 2.4f));
            sp.Add(new Vector2(0, 2.6f));
            sp.Add(new Vector2(2, 2.4f));
            sp.Add(new Vector2(2, 1.8f));
            sp.Add(new Vector2(0.7f, 1));

            _Create(x, y, sp, w);


            _body.FixedRotation = true;
                        
            InputManager.Channel[0].OnUp += delegate(float val)
            {
                _body.LinearVelocity = new Vector2(_body.LinearVelocity.X, -_currentSpeed * val);
            };

            InputManager.Channel[0].OnDown += delegate(float val)
            {
                _body.LinearVelocity = new Vector2(_body.LinearVelocity.X, _currentSpeed * val);
            };

            InputManager.Channel[0].OnLeft += delegate(float val)
            {
                _body.LinearVelocity = new Vector2(-_currentSpeed * val, _body.LinearVelocity.Y);
            };

            InputManager.Channel[0].OnRight += delegate(float val)
            {
                _body.LinearVelocity = new Vector2(_currentSpeed * val, _body.LinearVelocity.Y);
            };

            InputManager.Channel[0].OnAction1 += delegate(bool press)
            {
                _spawnBullets = press;
            };
        }





        public override void Update()
        {
            base.Update();

            if (_spawnBullets)
            {
                _createBullet();
            }

            //only calculate direction if spaceship is moving
            if (SMath.Abs(_body.LinearVelocity.Y) >= 3 || SMath.Abs(_body.LinearVelocity.X) >= 3)
            {
                float newAngle = (float)SMath.Atan2(_body.LinearVelocity.Y, _body.LinearVelocity.X) + (float) SMath.PI / 2f;
                _body.Rotation = newAngle;
            }
        }

        private Vector2 _GetDirectionNormal()
        {
            Vector2 normalDirection = _body.LinearVelocity;
            normalDirection.Normalize();
            return normalDirection;

        }


        private void _createBullet()
        {
            Vector2 directionNormal = _GetDirectionNormal();
            Vector2 position = _body.Position + directionNormal*5;
            Entity e = _currentWeapon.Shoot(position.X, position.Y, _world, directionNormal);
            if (e != null)
            {
                _eHolder.AddEntity(e);
            }
            
        }

    }
}
