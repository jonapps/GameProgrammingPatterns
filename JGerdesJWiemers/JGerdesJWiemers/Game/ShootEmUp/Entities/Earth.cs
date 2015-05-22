using FarseerPhysics;
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
    class Earth : SpriteEntity
    {

        private Animation _rotateAnimation;

        public Earth(float x, float y, World world, float radius) :
            base(world, AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_EARTH), 5 / ConvertUnits.ToSimUnits(64),  x, y, BodyType.Static)
        {
            _rotateAnimation = new Animation(0, 319, 20, true, false);
            _sprite.SetAnimation(_rotateAnimation);

            GravityController gravity = new GravityController(1f);
            gravity.AddBody(_body);
            gravity.Strength = 10;
            gravity.GravityType = GravityType.DistanceSquared;
            gravity.AddDisabledCategory(EntityCategory.SpaceShip);
            world.AddController(gravity);
            _body.Rotation = 23.44f * (float)SMath.PI / 180;
            _body.CollisionCategories = EntityCategory.Earth;


        }
    }
}
