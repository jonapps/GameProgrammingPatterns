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
        public enum Type{
            Rectangle = 0,
            Circle = 1,
            Polygon = 2
        }

        protected Texture _texture;
        protected int _width;
        protected int _height;
        protected Type _type;
        protected Vertices _vertices;
        protected float _radius;

        public TextureContainer(Texture texture)
        {
            _texture = texture;
            _type = Type.Rectangle;
            _width = (int)texture.Size.X;
            _height = (int)texture.Size.Y;
        }

        public TextureContainer(Texture texture, float[][] vertices)
            : this(texture, (int)texture.Size.X, (int)texture.Size.Y, vertices)
        {
            
        }

        public TextureContainer(Texture texture, float radius)
            : this(texture, (int)texture.Size.X, (int)texture.Size.Y, radius)
        {

        }

        public TextureContainer(Texture texture, int frameWidth, int frameHeight)
        {
            _texture = texture;
            _type = Type.Rectangle;
            _width = frameWidth;
            _height = frameHeight;
        }

        public TextureContainer(Texture texture, int frameWidth, int frameHeight, float[][] vertices)
        {
            _texture = texture;
            _type = Type.Polygon;
            _width = frameWidth;
            _height = frameHeight;
            _vertices = new Vertices();
            foreach(float[] point in vertices){
                _vertices.Add(new Vector2(point[0], point[1]));
            }

        }

        public TextureContainer(Texture texture, int frameWidth, int frameHeight, float radius)
        {
            _texture = texture;
            _type = Type.Circle;
            _width = frameWidth;
            _height = frameHeight;
            _radius = radius;
        }

        

    }
}
