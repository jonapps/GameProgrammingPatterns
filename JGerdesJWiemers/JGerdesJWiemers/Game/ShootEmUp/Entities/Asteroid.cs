using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Entities;
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
    class Asteroid : SpriteEntity
    {

        public Asteroid(World world, float x, float y, string textureName, float scale = 1, float xSpeed = 0, float ySpeed = 0, float rotSpeed = 0)
            : base(world, AssetLoader.Instance.getTexture(textureName), scale)
        {
            _body.CollisionCategories = EntityCategory.Asteroit;
            _body.LinearVelocity = new Vector2(xSpeed, ySpeed);
            _body.ApplyAngularImpulse(rotSpeed);
            _body.Mass = 100;
        }

        public class AsteroidDef : EntityDef
        {
            public enum Type
            {
                Asteroid1,
                Asteroid2,
                Asteroid3
            }
            public Type AsteroidType { get; set; }

            private static Dictionary<Type, String> types = new Dictionary<Type, string>()
            {
                {Type.Asteroid1, AssetLoader.TEXTURE_ASTEROID1},
                {Type.Asteroid2, AssetLoader.TEXTURE_ASTEROID2},
                {Type.Asteroid3, AssetLoader.TEXTURE_ASTEROID3}
            };


            public AsteroidDef(float xPos = 0, float yPos = 0, float xSpeed = 0, float ySpeed = 0, float scale = 1, float rotationSpeed = 0)
                : base(xPos, yPos, xSpeed, ySpeed, scale, rotationSpeed)
            {
                Random rand = new Random();
                Array values = Enum.GetValues(typeof(Type));
                AsteroidType = (Type)values.GetValue(rand.Next(values.Length));
            }

            public AsteroidDef(Type type, float xPos = 0, float yPos = 0, float xSpeed = 0, float ySpeed = 0, float scale = 1, float rotationSpeed = 0)
                : base(xPos, yPos, xSpeed, ySpeed, scale, rotationSpeed)
            {
                AsteroidType = type;
            }
            
            public override Engine.Entity Spawn(World world)
            {
                Asteroid asteroid = new Asteroid(world, Position.X, Position.Y, types[AsteroidType], Scale, Speed.X, Speed.Y, RotationSpeed);
                return asteroid;
            }
        }
    }
}
