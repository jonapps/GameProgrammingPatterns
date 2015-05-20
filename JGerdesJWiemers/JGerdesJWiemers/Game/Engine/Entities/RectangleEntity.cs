using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Entities
{
    class RectangleEntity : ShapeEntity
    {

        /// <summary>
        /// 
        /// </summary>
        public RectangleEntity(float x, float y, float width, float height, World w, float rotation = 0, BodyType bodyType = BodyType.Dynamic) : base(null)
        {
            _body = BodyFactory.CreateRectangle(w, width, height, 1f, new Vector2(x, y), rotation, bodyType, this);
            _renderShape = new RectangleShape(new Vector2f(width, height));
            _renderShape.Position = new Vector2f(x, y);
            _renderShape.Origin = new Vector2f(width / 2, height / 2);
        }


        public override void Update()
        {
            base.Update();
        }
    }
}
