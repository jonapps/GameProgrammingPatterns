using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using JGerdesJWiemers.Game.Engine.Exceptions;
using JGerdesJWiemers.Game.Engine.Shapes;
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
    class PolygonEntity : Entity
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="vertices"></param>
        /// <param name="w"></param>
        /// <param name="rotation"></param>
        /// <param name="bodyType"></param>
        public PolygonEntity(float x, float y, Vertices vertices,  World w, float rotation = 0, BodyType bodyType = BodyType.Dynamic) : base()
        {
            if (vertices.Count < 3)
            {
                throw new NotEnoughVerticesException();
            }
            _body = BodyFactory.CreatePolygon(w, vertices, 1f, new Vector2(x, y), rotation, bodyType, this);
            List<Vector2f> points = new List<Vector2f>();
            List <Vector2> pointsVec2 = vertices.ToList();
            for (int i = 0; i < pointsVec2.Count; ++i)
            {
                points.Add(_ConvertVectorToVector2f(pointsVec2[i]));
            }
            _renderShape = new PolygonShape(points);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
