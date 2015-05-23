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

namespace JGerdesJWiemers.Game.Engine
{
    abstract class Entity : IUpdateable, IRenderable
    {
        protected Body _body;
        protected Fixture _fixture;
        protected bool _deleteMe = false;
        protected bool _debug = Game.DEBUG;




        public Entity()
        {
        }

        #region Getter && Setter

        public bool DeleteMe { get { return _deleteMe; } }

        public Body Body { get{ return _body; } set { _body = value;} }

        public Fixture Fixture { set { _fixture = value; } get { return _fixture; } }


        #endregion

        public virtual void Update()
        {

        }

        public virtual void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            
        }


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


    }
}
