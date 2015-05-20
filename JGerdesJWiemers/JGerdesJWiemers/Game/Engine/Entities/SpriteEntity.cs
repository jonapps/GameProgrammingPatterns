﻿using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using JGerdesJWiemers.Game.Engine.Graphics;
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
        protected RenderStates _renderStates;

        public SpriteEntity(World world, TextureContainer textureContainer, float scale = 1, float x = 0, float y = 0, BodyType bodyType = BodyType.Dynamic) : 
            this(textureContainer, scale)
        {
            if (textureContainer is RectangleTextureContainer)
            {
                float w = ConvertUnits.ToSimUnits(textureContainer.Width * scale);
                float h = ConvertUnits.ToSimUnits(textureContainer.Height * scale);
                _body = BodyFactory.CreateRectangle(world, w, h, 1f, new Vector2(x, y), 0, bodyType, this);
                _fixture = FixtureFactory.AttachRectangle(w, h, 1f, new Vector2(0,0), _body, this);
            }
            else if (textureContainer is CircleTextureContainer)
            {
                float r = ((CircleTextureContainer)textureContainer).Radius;
                r = ConvertUnits.ToSimUnits(r * scale);
                _body = BodyFactory.CreateCircle(world, r, 1f, new Vector2(x, y), bodyType, this);
                _fixture = FixtureFactory.AttachCircle(r, 1f, _body, this);
            }
            else if (textureContainer is PolygonTextureContainer)
            {
                PolygonTextureContainer pTextureContainer = (PolygonTextureContainer)textureContainer;
                Vertices verts = new Vertices();
                foreach (Vector2 v in pTextureContainer.Vertices)
                {
                    verts.Add(new Vector2(ConvertUnits.ToSimUnits(v.X * scale), ConvertUnits.ToSimUnits(v.Y * scale)));
                }
                _body = BodyFactory.CreatePolygon(world, verts, 1f, new Vector2(x, y), 0, bodyType, this);
                _fixture = FixtureFactory.AttachPolygon(verts, 1f, _body, this);
            }

        }

        public SpriteEntity(TextureContainer textureContainer, float scale = 1)
            : base()
        {
            _sprite = new AnimatedSprite(textureContainer.Texture, textureContainer.Width, textureContainer.Height);
            _sprite.Origin = new Vector2f(textureContainer.Width / 2f, textureContainer.Height / 2f);
            _sprite.Scale = new Vector2f(ConvertUnits.ToSimUnits(scale), ConvertUnits.ToSimUnits(scale));
            _renderStates = new RenderStates(BlendMode.Alpha);
        }


        public override void Render(RenderTarget renderTarget, float extra)
        {
            if (_body != null)
            {
                _sprite.Position = _ConvertVectorToVector2f(_body.Position);
                _sprite.Rotation = _body.Rotation * 180 / (float) Math.PI;
            }

            _sprite.Draw(renderTarget, _renderStates);
            
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
