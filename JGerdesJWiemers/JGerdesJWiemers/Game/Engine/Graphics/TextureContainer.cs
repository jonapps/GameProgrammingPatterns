using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;

namespace JGerdesJWiemers.Game.Engine.Graphics
{
    class TextureContainer
    {
        public static readonly string IDENTIFIER = "none";
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Texture Texture { get; private set; }

        public TextureContainer(Texture texture)
            :this( texture, (int)texture.Size.X, (int)texture.Size.Y)
        {}

        public TextureContainer(Texture texture, int frameWidth, int frameHeight)
        {
            Texture = texture;
            Width = frameWidth;
            Height = frameHeight;
        }

    }

   
    class RectangleTextureContainer : TextureContainer
    {
        public static readonly string IDENTIFIER = "Rectangle";

         public RectangleTextureContainer(Texture texture, int frameWidth, int frameHeight)
            :base(texture, frameWidth, frameHeight)
         {}

         public RectangleTextureContainer(Texture texture)
             : base(texture)
         {}
    }

    class CircleTextureContainer : TextureContainer
    {
        public static readonly string IDENTIFIER = "Circle";
        public float Radius { get; private set; }

        public CircleTextureContainer(Texture texture, float radius)
            :base(texture)
        {
            Radius = radius;
        }

        public CircleTextureContainer(Texture texture, int frameWidth, int frameHeight, float radius)
            :base(texture, frameWidth, frameHeight)
        {
            Radius = radius;
        }
    }


    class PolygonTextureContainer : TextureContainer
    {
        public static readonly string IDENTIFIER = "Polygon";
        public Vertices Vertices { get; private set; }

        public PolygonTextureContainer(Texture texture, List<Vector2> vertices)
            :base(texture)
        {
            Vertices = (Vertices)vertices;
        }

        public PolygonTextureContainer(Texture texture, int frameWidth, int frameHeight, Vertices v)
            : base(texture, frameWidth, frameHeight)
        {
            Vertices = v;
        }
    }
}
