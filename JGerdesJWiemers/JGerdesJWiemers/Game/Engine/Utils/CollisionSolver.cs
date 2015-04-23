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
        private RenderWindow _window;
        public CollisionSolver(RenderWindow w)
        {
            _window = w;
        }

        public bool solve(RectangleEntity a, CircleEntity b)
        {
            return _SolveCircleRectangle(a, b);
        }



        private bool _SolveCircleRectangle(RectangleEntity a, CircleEntity b)
        {
            Vector2f p1, p2, normal, tangente, potencial;
            float bSpeedLength, x, y, z, pointToMiddle, pointToMiddleCorner;
            int j, i;
            Shape shape = a.Shape;

            // ball line stuff
            bSpeedLength = b.Speed.Length();
            Vector2f normalizedSpeed = new Vector2f(b.Speed.X / bSpeedLength, b.Speed.Y / bSpeedLength);
            Vector2f bSpeedNormal = new Vector2f(normalizedSpeed.Y * -1, normalizedSpeed.X);
            Vector2f currentTopPoint, currentBottomPoint, lastTopPoint, lastBottomPoint, intersectionPoint1, intersectionPoint2, intersectionPoint3, intersectionPoint4, intersectionPoint5;
            currentTopPoint = (bSpeedNormal * b.Radius) + (normalizedSpeed * b.Radius) + b.Position;
            currentBottomPoint = (bSpeedNormal * -1 * b.Radius) + (normalizedSpeed * b.Radius) + b.Position;
            lastTopPoint = (bSpeedNormal * b.Radius) + (normalizedSpeed * -1 * b.Radius) + b.LastPosition;
            lastBottomPoint = (bSpeedNormal * -1 * b.Radius) + (normalizedSpeed * -1 * b.Radius) + b.LastPosition;

            List<Vector2f> aLastPoints = a.LastPoints;
            List<Vector2f> aCurrentPoints = a.GetCurrentPoints();
            Vertex[] lineTop = 
            {
                new Vertex(lastTopPoint),
                new Vertex(currentTopPoint)
            };
            Vertex[] lineBot = 
            {
                new Vertex(lastBottomPoint),
                new Vertex(currentBottomPoint)
            };
            //debug
            //_window.Draw(lineBot, PrimitiveType.Lines);
            //_window.Draw(lineTop, PrimitiveType.Lines);
            //b.Render(_window, 0);
            //_window.Display();


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
                x = (potencial - p1).Length();
                y = (potencial - p2).Length();
                z = (p1 - p2).Length();
                pointToMiddle = x + y - z;
                if (pointToMiddle < 0.4f)
                {

                    b.Position -= normal * (pointToMiddle - 1) * -1;
                    b.Speed = JGerdesJWiemers.Game.Engine.Utils.Math.Scalar(tangente, b.Speed) * tangente - JGerdesJWiemers.Game.Engine.Utils.Math.Scalar(normal, b.Speed) * normal;
                    b.RotationSpeed = pointToMiddle * pointToMiddle * 40;
                    return true;
                }
                pointToMiddleCorner = (p1 - b.Position).Length() - b.Radius;
                if (pointToMiddle < 0.4f)
                {
                    b.Position -= (normal * (pointToMiddleCorner - 1) * -1f);
                    b.Speed = JGerdesJWiemers.Game.Engine.Utils.Math.Scalar(tangente, b.Speed) * tangente - JGerdesJWiemers.Game.Engine.Utils.Math.Scalar(normal, b.Speed) * normal;
                    return true;
                }

                intersectionPoint1 = JGerdesJWiemers.Game.Engine.Utils.Math.TestIntersection(currentTopPoint, lastTopPoint, aCurrentPoints[i], aLastPoints[i]);
                intersectionPoint2 = JGerdesJWiemers.Game.Engine.Utils.Math.TestIntersection(currentBottomPoint, lastBottomPoint, aCurrentPoints[i], aLastPoints[i]);
                intersectionPoint3 = JGerdesJWiemers.Game.Engine.Utils.Math.TestIntersection(currentTopPoint, lastTopPoint, aCurrentPoints[i], aCurrentPoints[j]);
                intersectionPoint4 = JGerdesJWiemers.Game.Engine.Utils.Math.TestIntersection(currentBottomPoint, lastBottomPoint, aLastPoints[i], aLastPoints[j]);
                intersectionPoint5 = JGerdesJWiemers.Game.Engine.Utils.Math.TestIntersection(b.LastPosition, b.GetCurrentPosition(), aLastPoints[i], aLastPoints[j]);
                if (!_IsVectorNull(intersectionPoint1) || !_IsVectorNull(intersectionPoint2) || !_IsVectorNull(intersectionPoint3) || !_IsVectorNull(intersectionPoint4) || !_IsVectorNull(intersectionPoint5))
                {
                    b.Position = b.LastPosition - (normal * (pointToMiddle - 1));
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
