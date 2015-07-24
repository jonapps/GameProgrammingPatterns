using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.System;
using Microsoft.Xna.Framework;

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

        public static float Length2(this Vector2i vec)
        {
            return vec.X * vec.X + vec.Y * vec.Y;
        }

        public static Vector2f ToVector2f(this Vector2 v)
        {
            return new Vector2f(v.X, v.Y);
        }

        public static Vector2 ToVector2(this Vector2f v)
        {
            return new Vector2(v.X, v.Y);
        }

        public static Vector2f ToVector2f(this Vector2i v){
            return new Vector2f(v.X, v.Y);
        }

    }
}
