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

            int j, i;
            Shape shape = a.Shape;
            for (i = 0; i < a.Shape.GetPointCount(); ++i)
            {

                if (i == shape.GetPointCount() - 1)
                {
                    j = 0;
                }
                else
                {
                    j = i + 1;
                }
                p1 = a.GetCurrentPoints()[i];
                p2 = a.GetCurrentPoints()[j]; 
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
                float bSpeedLength = b.Speed.Length();
                Vector2f normalizedSpeed = new Vector2f(b.Speed.X / bSpeedLength, b.Speed.Y / bSpeedLength);
                Vector2f bSpeedNormal = new Vector2f(normalizedSpeed.X, normalizedSpeed.Y * -1);
                Vector2f currentTopPoint, currentBottomPoint, lastTopPoint, lastBottomPoint, intersectionPoint1, intersectionPoint2, intersectionPoint3, intersectionPoint4;
                currentTopPoint = (bSpeedNormal * b.Radius) + (normalizedSpeed * b.Radius) + b.Position;
                currentBottomPoint = (bSpeedNormal * -1 * b.Radius) + (normalizedSpeed * b.Radius) + b.Position;
                lastTopPoint = (bSpeedNormal * b.Radius) + (normalizedSpeed * -1 * b.Radius) + b.Position;
                lastBottomPoint = (bSpeedNormal * -1 * b.Radius) + (normalizedSpeed * -1 * b.Radius) + b.Position;

                List<Vector2f> aLastPoints = a.LastPoints;
                List<Vector2f> aCurrentPoints = a.GetCurrentPoints();

                intersectionPoint1 = JGerdesJWiemers.Game.Engine.Utils.Math.TestIntersection(currentTopPoint, lastTopPoint, aCurrentPoints[i], aLastPoints[i]);
                intersectionPoint2 = JGerdesJWiemers.Game.Engine.Utils.Math.TestIntersection(currentTopPoint, lastTopPoint, aCurrentPoints[i], aLastPoints[i]);
                intersectionPoint3 = JGerdesJWiemers.Game.Engine.Utils.Math.TestIntersection(currentTopPoint, lastTopPoint, aCurrentPoints[i], aCurrentPoints[j]);
                intersectionPoint4 = JGerdesJWiemers.Game.Engine.Utils.Math.TestIntersection(currentTopPoint, lastTopPoint, aLastPoints[i], aLastPoints[j]);
                if (!_IsVectorNull(intersectionPoint1) || !_IsVectorNull(intersectionPoint2) || !_IsVectorNull(intersectionPoint3) || !_IsVectorNull(intersectionPoint4))
                {
                    b.Position = b.LastPosition - (normal * (pointToMiddle - 1) * -1); 
                    b.Speed = JGerdesJWiemers.Game.Engine.Utils.Math.Scalar(tangente, b.Speed) * tangente - JGerdesJWiemers.Game.Engine.Utils.Math.Scalar(normal, b.Speed) * normal;
                    b.RotationSpeed = pointToMiddle * pointToMiddle * 40;
                    return true;
                }

            }
            
            return false;
        }

        private bool _IsVectorNull(Vector2f v)
        {
            if (v.X == 0 && v.Y == 0)
            {
                return true;
            }
            return false;
        }
    }
}
