using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.Engine.Graphics;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using JGerdesJWiemers.Game.Engine.Interfaces;
using FarseerPhysics;

namespace JGerdesJWiemers.Game.Engine
{
    abstract class Entity : IDrawable
    {
        protected Body _body;
        protected Fixture _fixture;
        protected bool _deleteMe = false;
        protected bool _debug = Game.DEBUG;
        protected int _health = 100;
                
        protected Vector2f _ConvertVector2ToVector2f(Vector2 vec)
        {
            return new Vector2f(vec.X, vec.Y);
        }

        protected Vector2 _ConvertVector2fToVector2(Vector2f vec)
        {
            return new Vector2(vec.X, vec.Y);
        }

        public float Z
        {
            get
            {
                return _body.WorldCenter.X * _body.WorldCenter.Y;
            }
        }

        public Vector2 Position {
            get
            {
                return _body.WorldCenter;
            }
        }

        public Boolean DeleteMe
        {
            get
            {
                return _deleteMe;
            }
        }


        public abstract class EntityDef
        {
            public Vector2 Position { get; set; }
            public float Scale { get; set; }
            public Vector2 Speed { get; set; }
            public float RotationSpeed { get; set; }

            public EntityDef(float xPosition = 0, float yPosition = 0, float xSpeed = 0, float ySpeed = 0, float scale = 1, float rotationSpeed = 0)
            {
                Position = new Vector2(xPosition, yPosition);
                Speed = new Vector2(xSpeed, ySpeed);
                Scale = scale;
                RotationSpeed = rotationSpeed;
            }

            public abstract Entity Spawn(World world);
        }


        /// <summary>
        /// To Apply dmg to any entity 
        /// </summary>
        /// <param name="dmg"></param>
        public virtual void ApplyDamage(int dmg)
        {
            _health -= dmg;
        }


        public abstract void PastUpdate();

        public abstract void PreDraw(float extra);

        public abstract void Draw(RenderTarget target, RenderStates states);

        public abstract void Update();
    }

}
