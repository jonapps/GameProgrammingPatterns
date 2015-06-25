using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Shapes;
using JGerdesJWiemers.Game.TowerDefence;
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
    class SpriteEntity : Entity
    {
        protected AnimatedSprite _sprite;
        private Shape _colliderShape;

        public SpriteEntity(World world, TextureContainer textureContainer, float scale = 1, float x = 0, float y = 0, BodyType bodyType = BodyType.Dynamic) : 
            this(textureContainer, scale)
        {
            if (textureContainer is RectangleTextureContainer)
            {
                RectangleTextureContainer rectCont = textureContainer as RectangleTextureContainer;
                float w = ConvertUnits.ToSimUnits(rectCont.TileSize.X * scale);
                float h = ConvertUnits.ToSimUnits(rectCont.TileSize.Y * scale);
                _body = BodyFactory.CreateRectangle(world, w, h, 1f, new Vector2(0, 0), 0, bodyType, this);
                _fixture = FixtureFactory.AttachRectangle(w, h, 1f, new Vector2(0, 0), _body, this);

                w = rectCont.TileSize.X * scale;
                h = rectCont.TileSize.Y * scale;
                List<Vector2f> points = new List<Vector2f>();
                points.Add(Map.MapToScreen(0,0));
                points.Add(Map.MapToScreen(w, 0));
                points.Add(Map.MapToScreen(w, h));
                points.Add(Map.MapToScreen(0, h));
                _colliderShape = new PolygonShape(points);
                _colliderShape.Origin = Map.MapToScreen(w/2, h/2);
            }
            else if (textureContainer is CircleTextureContainer)
            {
                float r = ((CircleTextureContainer)textureContainer).Radius;
                r = ConvertUnits.ToSimUnits(r * scale);
                _body = BodyFactory.CreateCircle(world, r, 1f, new Vector2(x, y), bodyType, this);
                _fixture = FixtureFactory.AttachCircle(r, 1f, _body, this);

                r = ((CircleTextureContainer)textureContainer).Radius * scale;
                List<Vector2f> points = new List<Vector2f>();
                int pointCount = 20;
                double degPerPoint = (Math.PI * 2) / pointCount;
                for (int i = 0; i < pointCount; i++)
                {
                    points.Add(Map.MapToScreen((float)Math.Cos(i*degPerPoint) * r, (float)Math.Sin(i*degPerPoint) * r));
                }
                _colliderShape = new PolygonShape(points);
            }
 
        }

        public SpriteEntity(TextureContainer textureContainer, float scale = 1)
            : base()
        {
            _sprite = new AnimatedSprite(textureContainer.Texture, textureContainer.Width, textureContainer.Height);
            _sprite.Origin = new Vector2f(textureContainer.Width / 2f, textureContainer.Height);
            if (textureContainer is CircleTextureContainer)
            {
                _sprite.Origin = _ConvertVector2ToVector2f((textureContainer as CircleTextureContainer).Center);
            }
            if (textureContainer is RectangleTextureContainer)
            {
                _sprite.Origin = _ConvertVector2ToVector2f((textureContainer as RectangleTextureContainer).Center);
            }
            
            _sprite.Scale = new Vector2f(scale, scale);

        }

        public override void Update()
        {
            _sprite.Update();
        }

        public override void PastUpdate()
        {
            
        }

        public override void PreDraw(float extra)
        {
            Vector2 positionInPixel = getPositionInPixel();
            _sprite.Position = Map.MapToScreen(positionInPixel.X, positionInPixel.Y);
            _colliderShape.Position = Map.MapToScreen(positionInPixel.X, positionInPixel.Y);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (Game.DEBUG)
                target.Draw(_colliderShape);
            target.Draw(_sprite, states);
        }

        protected Vector2 getPositionInPixel()
        {
            return ConvertUnits.ToDisplayUnits(_body.WorldCenter);
        }

    }
}
