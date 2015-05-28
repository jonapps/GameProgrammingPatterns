using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Entities;
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
    class Asteroid : SpriteEntity
    {
        public delegate void AstroidSplit(List<Asteroid> la);
        public event AstroidSplit OnSplit;

        private static int SPLIT_EXPLOSION_SPEED_MULTIPLIER = 5;
        private static int SPLIT_MIN_CHILDS = 2;
        private static int SPLIT_MAX_CHILDS = 3;
        private int _splitLevel;
        private float _scale;
        private bool _hadImpact = false;


        public Asteroid(World world, float x, float y, string textureName, float scale = 1, float xSpeed = 0, float ySpeed = 0, float rotSpeed = 0, int splitLevel = 0)
            : base(world, AssetLoader.Instance.getTexture(textureName), scale)
        {
            
            _splitLevel = splitLevel;
            Console.WriteLine(_splitLevel);
            _body.CollisionCategories = EntityCategory.Asteroit;
            _body.LinearVelocity = new Vector2(xSpeed, ySpeed);
            _body.ApplyAngularImpulse(rotSpeed);
            _body.Mass = 34 * (_splitLevel + 1);
            _body.Position = new Vector2(x, y) - _body.LocalCenter;
            _scale = scale;
            _health = 5 * (_splitLevel + 1);
            _fixture.OnCollision += _OnCollision;
        }

        private bool _OnCollision(Fixture fa, Fixture fb, Contact contact)
        {
            if (_deleteMe)
            {
                contact.Enabled = false;
                return false;
            }
            if (fb.Body.UserData is Earth && !_hadImpact)
            {
                Earth earth = fb.Body.UserData as Earth;
                _hadImpact = true;
                earth.ApplyDamage((int)_body.Mass);
                // doesnt work?.. dont know why
                //fa.Body.Enabled = false;
                _deleteMe = true;
            }
            else if (fb.Body.UserData is SpaceShip && !_hadImpact)
            {
                SpaceShip sp = fb.Body.UserData as SpaceShip;
                sp.ApplyDamage((int)_body.Mass);
                _deleteMe = true;
            }
            return true;
        }



        internal override void PastUpdate()
        {
            base.PastUpdate();
            if (_health <= 0)
            {
                _Split();
            }
        }

        private void _Split()
        {
            if (_splitLevel > 0)
            {
                
                List<Asteroid> newAsteroids = new List<Asteroid>();
                AsteroidDef def;
                Random rand = new Random();
                int count = rand.Next(SPLIT_MIN_CHILDS, SPLIT_MAX_CHILDS);
                for (int i = 0; i < count; i++)
                {
                    double degree = i * (SMath.PI * 2) / count;
                    Vector2 speed = new Vector2((float)SMath.Cos(degree), (float)SMath.Sin(degree));
                    def = new AsteroidDef(_body.WorldCenter.X + speed.X, _body.WorldCenter.Y + speed.Y,
                        speed.X * SPLIT_EXPLOSION_SPEED_MULTIPLIER, speed.X * SPLIT_EXPLOSION_SPEED_MULTIPLIER, _splitLevel - 1, _scale / 2f, 0);
                    newAsteroids.Add((Asteroid)EntityFactory.Instance.Spawn(def));
                }
                OnSplit(newAsteroids);
            }
            GameManager.Instance.AddScore(100);
            _deleteMe = true;
            _splitLevel = 0;
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
