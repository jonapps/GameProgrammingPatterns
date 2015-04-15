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
    }
}
