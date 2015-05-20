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




        public Entity()
        {
        }

        #region Getter && Setter

        public bool DeleteMe { get { return _deleteMe; } }

        public Body Body { get{ return _body; } set { _body = value;} }

        public Fixture Fixture { set { _fixture = value; } get { return _fixture; } }

        public Shape RenderShape { get { return _renderShape; } }

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
    }
}
