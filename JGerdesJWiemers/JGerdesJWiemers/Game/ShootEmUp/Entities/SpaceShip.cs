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

namespace JGerdesJWiemers.Game.ShootEmUp.Entities
{
    class SpaceShip : SpriteEntity
    {

        private static readonly float SPEED_DEFAULT = 40;

        private float _currentSpeed = SPEED_DEFAULT;
        private EntityHolder _eHolder;
        private bool _spawnBullets = false;
        private World _world;


        public SpaceShip(float x, float y , World w, EntityHolder gscreen)
            : base(w, AssetLoader.Instance.LoadTexture(AssetLoader.TEXTURE_SPACESHIP), 1, x, y)
        {
            _eHolder = gscreen;
            _world = w;
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
            _eHolder.AddEntity(new Bullet(position.X, position.Y, _world, directionNormal));
        }

    }
}
