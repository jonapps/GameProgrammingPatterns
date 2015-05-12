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

namespace JGerdesJWiemers.Game.ShootEmUp.Entities
{
    class SpaceShip : SpriteEntity
    {

        private static readonly float SPEED_DEFAULT = 40;

        private float _currentSpeed = SPEED_DEFAULT;

        //need to animate rotation angle without having jumps from -180 to 180
        private float _lastAngle;

        public SpaceShip(float x, float y , World w)
            : base(AssetLoader.Instance.LoadTexture(AssetLoader.TEXTURE_SPACESHIP), 32, 48)
        {
            _sprite.Origin = new Vector2f(16, 24);
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

            

        }

        public override void Update()
        {
            base.Update();

            //only calculate direction if spaceship is moving
            if (SMath.Abs(_body.LinearVelocity.Y) >= 3 || SMath.Abs(_body.LinearVelocity.X) >= 3)
            {
                float newAngle = (float)SMath.Atan2(_body.LinearVelocity.Y, _body.LinearVelocity.X) + (float) SMath.PI / 2f;
                if (_lastAngle <= 0 && newAngle > SMath.PI)
                    newAngle -= (float) SMath.PI * 2;
                if (_lastAngle >= SMath.PI && newAngle < 0)
                    newAngle += (float)SMath.PI * 2;

                Console.WriteLine(newAngle * 180 / SMath.PI);

                _lastAngle = newAngle;

                _body.Rotation += (newAngle - _body.Rotation) /5f;
            }
        }
    }
}
