﻿using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
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

namespace JGerdesJWiemers.Game.ShootEmUp.Entities
{
    class Astronaut: SpriteEntity
    {

        private Animation _rotateAnimation;
        private int damage = 100;

        public Astronaut(World world, Vector2f position, float xSpeed = 0, float ySpeed = 0, float rotSpeed = 0)
            : base(world, AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_ASTRONAUT), 0.5f, position.X, position.Y)
        {
            _rotateAnimation = new Animation(0, 70, 20, true, false);
            _sprite.SetAnimation(_rotateAnimation);

            //_body.ApplyLinearImpulse(new Vector2(10f, 1f));
            _body.LinearVelocity = new Vector2(xSpeed, ySpeed);
            _body.AngularVelocity = rotSpeed;
            _body.Mass = 1;
        }

        public class AstronautDef : EntityDef
        {
            public AstronautDef(float xPos = 0, float yPos = 0, float xSpeed = 0, float ySpeed = 0, float scale = 1, float rotationSpeed = 0)
                : base(xPos, yPos, xSpeed, ySpeed, scale, rotationSpeed)
            { }
            public override Engine.Entity Spawn(World world)
            {
                Astronaut astronaut = new Astronaut(world, new Vector2f(Position.X, Position.Y), Speed.X, Speed.Y, RotationSpeed);
                return astronaut;
            }
        }

    }
}
