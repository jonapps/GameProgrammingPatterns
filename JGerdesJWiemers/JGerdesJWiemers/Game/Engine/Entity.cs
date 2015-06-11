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

namespace JGerdesJWiemers.Game.Engine
{
    abstract class Entity : IDrawable
    {
        protected Body _body;
        protected Fixture _fixture;
        protected bool _deleteMe = false;
        protected bool _debug = Game.DEBUG;
        protected int _health = 100;

        #region Getter && Setter

        public bool DeleteMe { get { return _deleteMe; } set { _deleteMe = value; } }

        public Body Body { get{ return _body; } set { _body = value;} }

        public Fixture Fixture { set { _fixture = value; } get { return _fixture; } }


        #endregion

        
        protected Vector2f _ConvertVectorToVector2f(Vector2 vec)
        {
            return new Vector2f(vec.X, vec.Y);
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


        public virtual void PastUpdate()
        {

        }

        public virtual void PreDraw(float extra)
        {

        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            Render(target, 0);
        }

        //TODO: remove this
        public virtual void Render(RenderTarget target, float extra)
        {

        }

        public virtual void Update()
        {

        }
    }
}
