using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Entities;
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
    class Asteroid : SpriteEntity
    {

        private static int SPLIT_EXPLOSION_SPEED_MULTIPLIER = 5;
        private static int SPLIT_MIN_CHILDS = 2;
        private static int SPLIT_MAX_CHILDS = 3;

        private int _splitLevel;
        private float _scale;


        public Asteroid(World world, float x, float y, string textureName, float scale = 1, float xSpeed = 0, float ySpeed = 0, float rotSpeed = 0, int splitLevel = 0)
            : base(world, AssetLoader.Instance.getTexture(textureName), scale)
        {
            _body.CollisionCategories = EntityCategory.Asteroit;
            _body.LinearVelocity = new Vector2(xSpeed, ySpeed);
            //_body.ApplyAngularImpulse(rotSpeed);
            //_body.Mass = 100;
            _body.Position = new Vector2(x, y) - _body.LocalCenter;
            _body.OnCollision +=_OnCollision;
            _splitLevel = splitLevel;
            _scale = scale;
        }

        bool _OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            if (fixtureA.Body.UserData is Bullet || fixtureB.Body.UserData is Bullet)
            {
                if (_splitLevel > 0)
                {
                    AsteroidDef def;
                    Random rand = new Random();
                    int count = rand.Next(SPLIT_MIN_CHILDS, SPLIT_MAX_CHILDS);
                    for (int i = 0; i < count; i++)
                    {
                        double degree = i * (SMath.PI * 2) / count;
                        Vector2 speed = new Vector2((float)SMath.Cos(degree), (float)SMath.Sin(degree));
                        def = new AsteroidDef(_body.WorldCenter.X + speed.X, _body.WorldCenter.Y + speed.Y, 
                            speed.X * SPLIT_EXPLOSION_SPEED_MULTIPLIER, speed.X * SPLIT_EXPLOSION_SPEED_MULTIPLIER, _splitLevel - 1, _scale / 2f, 0);
                        EntityFactory.Instance.Spawn(def);
                    }
                }
                _deleteMe = true;
                _splitLevel = 0;
            }
            return true;
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

            public int SplitLevel { get; set; }

            private static Dictionary<Type, String> types = new Dictionary<Type, string>()
            {
                {Type.Asteroid1, AssetLoader.TEXTURE_ASTEROID1},
                {Type.Asteroid2, AssetLoader.TEXTURE_ASTEROID2},
                {Type.Asteroid3, AssetLoader.TEXTURE_ASTEROID3}
            };


            public AsteroidDef(float xPos = 0, float yPos = 0, float xSpeed = 0, float ySpeed = 0, int splitLevel = 0, float scale = 1, float rotationSpeed = 0)
                : base(xPos, yPos, xSpeed, ySpeed, scale, rotationSpeed)
            {
                Random rand = new Random();
                Array values = Enum.GetValues(typeof(Type));
                AsteroidType = (Type)values.GetValue(rand.Next(values.Length));
                SplitLevel = splitLevel;
            }

            public AsteroidDef(Type type, float xPos = 0, float yPos = 0, float xSpeed = 0, float ySpeed = 0, int splitLevel = 0, float scale = 1, float rotationSpeed = 0)
                : base(xPos, yPos, xSpeed, ySpeed, scale, rotationSpeed)
            {
                AsteroidType = type;
                SplitLevel = splitLevel;
            }
            
            public override Engine.Entity Spawn(World world)
            {
                Asteroid asteroid = new Asteroid(world, Position.X, Position.Y, types[AsteroidType], Scale, Speed.X, Speed.Y, RotationSpeed, SplitLevel);
                return asteroid;
            }
        }
    }
}
