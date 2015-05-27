﻿using FarseerPhysics.Common;
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
using FarseerPhysics.Controllers;
using FarseerPhysics;

namespace JGerdesJWiemers.Game.ShootEmUp.Entities
{
    class SpaceShip : SpriteEntity, InputHandler
    {

        private static readonly float SPEED_DEFAULT = 40;
        private Weapon _currentWeapon;

        private float _currentSpeed = SPEED_DEFAULT;
        private EntityHolder _eHolder;
        private bool _spawnBullets = false;
        private World _world;
        private InputMapper _input;

        private int _bulletSpace = 1000;


        public SpaceShip(float x, float y , World w, EntityHolder gscreen)
            : base(w, AssetLoader.Instance.LoadTexture(AssetLoader.TEXTURE_SPACESHIP), 1, x, y)
        {
            _input = new InputMapper();
            _fixture.CollisionCategories = EntityCategory.SpaceShip;
            _currentWeapon = new DoubleGatlinGun();
            _sprite.Origin = new Vector2f(16, 24);
            _eHolder = gscreen;
            _world = w;
            _body.FixedRotation = true;
            _body.IgnoreGravity = true;

            _input.On("up", delegate(InputEvent e, int channel)
            {
                _body.LinearVelocity = new Vector2(_body.LinearVelocity.X, -_currentSpeed * ((JoystickEvent)e).Value);
                return true;
            });

            _input.On("down", delegate(InputEvent e, int channel)
            {
                _body.LinearVelocity = new Vector2(_body.LinearVelocity.X, _currentSpeed *  ((JoystickEvent)e).Value);
                return true;
            });

            _input.On("left", delegate(InputEvent e, int channel)
            {
                _body.LinearVelocity = new Vector2(-_currentSpeed * ((JoystickEvent)e).Value, _body.LinearVelocity.Y);
                return true;
            });

            _input.On("right", delegate(InputEvent e, int channel)
            {
                _body.LinearVelocity = new Vector2(_currentSpeed * ((JoystickEvent)e).Value, _body.LinearVelocity.Y);
                return true;
            });

            _input.On("shoot", delegate(InputEvent e, int channel)
            {
                _spawnBullets = ((KeyEvent)e).Pressed;
                return true;
            });
            _input.On("weaponSwitch", delegate(InputEvent e, int channel)
            {
                if (_currentWeapon is DoubleGatlinGun)
                {
                    _currentWeapon = new RocketLauncher();
                }
                else if (_currentWeapon is GatlinGun)
                {
                    _currentWeapon = new DoubleGatlinGun();
                }
                else
                {
                    _currentWeapon = new GatlinGun();
                }
                return true;
            });
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


        private void _createBullet()
        {
            Vector2 directionNormal = new Vector2((float)System.Math.Cos(_body.Rotation - System.Math.PI / 2), (float)System.Math.Sin(_body.Rotation - System.Math.PI / 2));
            Vector2 position = _body.Position + directionNormal*1;
            List<Entity> el = _currentWeapon.Shoot(position.X, position.Y, _world, directionNormal, _body.Rotation);
            foreach (Entity e in el){
                _eHolder.AddEntity(e);
            }
        }


        public bool OnInputEvent(string name, InputEvent e, int channel)
        {
            return _input.OnInputEvent(name, e, channel);
        }
    }
}
