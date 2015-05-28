using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Controllers;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.ShootEmUp.Logic;
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
        public delegate void EarthEventHandler();
        public event EarthEventHandler OnEarthDestroyed;
        private Animation _rotateAnimation;

        public Earth(World world, float x, float y, float radius) :
            base(world, AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_EARTH), 5 / ConvertUnits.ToSimUnits(64),  x, y, BodyType.Static)
        {
            _rotateAnimation = new Animation(0, 319, 20, true, false);
            _sprite.SetAnimation(_rotateAnimation);

            GravityController gravity = new GravityController(1f);
            gravity.AddBody(_body);
            gravity.Strength = 1;
            gravity.GravityType = GravityType.DistanceSquared;
            gravity.AddDisabledCategory(EntityCategory.SpaceShip);
            world.AddController(gravity);
            _body.Rotation = 23.44f * (float)SMath.PI / 180;
            _body.CollisionCategories = EntityCategory.Earth;
            GameManager.Instance.SetEarthHealth(_health);

        }

        public override void ApplyDamage(int dmg)
        {
            base.ApplyDamage(dmg);
            Console.WriteLine("Earth health: "+_health);
            GameManager.Instance.SetEarthHealth(_health);
            if (_health <= 0)
            {
                OnEarthDestroyed();
            }
        }





        public class EarthDef : EntityDef
        {
            public float Radius { get; set; }
            public EarthDef(float radius, float xPos = 0, float yPos = 0, float xSpeed = 0, float ySpeed = 0, float scale = 1, float rotationSpeed = 0)
                : base(xPos, yPos, xSpeed, ySpeed, scale, rotationSpeed)
            {
                Radius = radius;
            }

            public override Engine.Entity Spawn(World world)
            {
                Earth earth = new Earth(world, Position.X, Position.Y, Radius);
                return earth;
            }
        }
    }
}
