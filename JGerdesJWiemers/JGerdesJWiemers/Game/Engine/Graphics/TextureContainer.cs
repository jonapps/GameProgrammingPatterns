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
        public static readonly string IDENTIFIER = "Tile";
        public Vector2 Center { get; private set; }
        public Vector2 TileSize { get; private set; }

        public RectangleTextureContainer(Texture texture, int frameWidth, int frameHeight, Vector2 center, Vector2 tileSize)
            : base(texture, frameWidth, frameHeight)
        {
            Center = center;
            TileSize = tileSize;
        }

        public RectangleTextureContainer(Texture texture, Vector2 center, Vector2 tileSize)
            : base(texture)
        {
            Center = center;
            TileSize = tileSize;
        }
    }

    class CircleTextureContainer : TextureContainer
    {
        public static readonly string IDENTIFIER = "Circle";
        public Vector2 Center { get; private set; }
        public float Radius { get; private set; }

        public CircleTextureContainer(Texture texture, Vector2 center, float radius)
            : base(texture)
        {
            Center = center;
            Radius = radius;
        }

        public CircleTextureContainer(Texture texture, int frameWidth, int frameHeight, Vector2 center, float radius)
            : base(texture, frameWidth, frameHeight)
        {
            Center = center;
            Radius = radius;
        }
    }


}
