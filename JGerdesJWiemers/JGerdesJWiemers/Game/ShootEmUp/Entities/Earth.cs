﻿using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Controllers;
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
using SMath = System.Math;

namespace JGerdesJWiemers.Game.ShootEmUp.Entities
{
    class Earth : SpriteCircleEntity
    {

        private Animation _rotateAnimation;

        public Earth(float x, float y, World world, float radius) :
            base(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_EARTH), 128, 128, x, y, radius, world, BodyType.Static)
        {
            _rotateAnimation = new Animation(0, 319, 20, true, false);
            _sprite.SetAnimation(_rotateAnimation);

            GravityController gravity = new GravityController(2f);
            gravity.AddBody(_body);
            world.AddController(gravity);
            _body.Rotation = 23.44f * (float)SMath.PI / 180;


        }
    }
}
