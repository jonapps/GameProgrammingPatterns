using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Helper;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Utils
{
    class CollisionSolver
    {
        public CollisionSolver()
        {
            
        }

        public bool solve(RectangleEntity a, CircleEntity b)
        {
            return _SolveCircleRectangle(a, b);
        }



        private bool _SolveCircleRectangle(RectangleEntity a, CircleEntity b)
        {
            Vector2f p1, p2, normal, tangente, potencial;

            uint j;
            Shape shape = a.Shape;
            for (uint i = 0; i < a.Shape.GetPointCount(); ++i)
            {

                if (i == shape.GetPointCount() - 1)
                {
                    j = 0;
                }
                else
                {
                    j = i + 1;
                }
                p1 = shape.GetPoint(i);
                p1 = shape.Transform.TransformPoint(p1);
                p2 = shape.GetPoint(j); 
                p2 = shape.Transform.TransformPoint(p2);
                tangente = p2 - p1;
                tangente /= tangente.Length();
                normal = new Vector2f(tangente.Y, tangente.X * -1) * -1;
                potencial = b.Position + normal * b.Radius;
                float x = (potencial - p1).Length();
                float y = (potencial - p2).Length();
                float z = (p1 - p2).Length();
                float pointToMiddle = x + y - z;
                if (pointToMiddle < 0.4f)
                {
                    float speedLength = b.Speed.Length();
                    b.Position -= normal * (pointToMiddle - 1) * -1;
                    b.Speed = JGerdesJWiemers.Game.Engine.Utils.Math.Scalar(tangente, b.Speed) * tangente - JGerdesJWiemers.Game.Engine.Utils.Math.Scalar(normal, b.Speed) * normal;
                    b.RotationSpeed = pointToMiddle * pointToMiddle * 40;
                    return true;
                }
                pointToMiddle = (p1 - b.Position).Length() - b.Radius;
                if (pointToMiddle < 0.4f)
                {
                    b.Position -= (normal * (pointToMiddle - 1) * -1f);
                    b.Speed = JGerdesJWiemers.Game.Engine.Utils.Math.Scalar(tangente, b.Speed) * tangente - JGerdesJWiemers.Game.Engine.Utils.Math.Scalar(normal, b.Speed) * normal;
                    return true;
                }
            }
            return false;
        }
    }
}
