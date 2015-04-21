using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.System;

namespace JGerdesJWiemers.Game.Engine.Utils
{
    public static class Math
    {
        public static float Length2(this Vector2f vec)
        {
            return vec.X * vec.X + vec.Y * vec.Y;
        }

        public static float Length(this Vector2f vec)
        {
            return (float) System.Math.Sqrt(vec.Length2());
        }

        public static float DistanceTo(this Vector2f v1, Vector2f v2)
        {
            return (v1 - v2).Length();
        }

        public static float Distance2To(this Vector2f v1, Vector2f v2)
        {
            return (v1 - v2).Length2();
        }



        public static float Scalar(Vector2f v1, Vector2f v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y; 
        }


        public static Vector2f TestIntersection(Vector2f start1, Vector2f end1, Vector2f start2, Vector2f end2)
        {
            float d = ((end2.Y - start1.Y) * (end1.X - start1.X));
            d -= ((end2.X - start1.X) * (end1.Y - start1.Y));

            if (d == 0)
            {
                return default(Vector2f);
            }

            float s = start1.Y - start2.Y;
            float t = start1.X - start2.X;

            float n1 = ((end2.X - start2.X) * s) - ((end2.Y - start2.Y) * t);
            float n2 = ((end1.X - start1.X) * s) - ((end1.Y - start1.Y) * t);

            s = n1 / d;
            t = n2 / d;

            if (s > 0 && s < 1 && t > 0 && t < 1)
            {
                return start1 + s * (end1 - start1);
            }
            else
            {
                return default(Vector2f);
            }

        }

    }
}
