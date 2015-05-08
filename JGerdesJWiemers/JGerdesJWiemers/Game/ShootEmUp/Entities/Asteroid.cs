using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Utils;
using Microsoft.Xna.Framework;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Entities
{
    class Asteroid : SpriteEntity
    {
        
        public Asteroid(float x, float y, World world, float scale = 1, float xSpeed = 0, float ySpeed = 0, float rotSpeed = 0) 
            : base(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_ASTEROID1), 128, 76)
        {
            Vertices vertices = new Vertices();
            float w = ConvertUnits.ToSimUnits(scale * 128);
            float h = ConvertUnits.ToSimUnits(scale * 76);
            vertices.Add(new Vector2(0, 0));
            vertices.Add(new Vector2(w, 0));
            vertices.Add(new Vector2(w, h));
            vertices.Add(new Vector2(0, h));

            _Create(x, y, vertices, world);

            _sprite.Scale = new Vector2f(_sprite.Scale.X * scale, _sprite.Scale.Y * scale);

            _body.LinearVelocity = new Vector2(xSpeed, ySpeed);
            _body.AngularVelocity = rotSpeed;
            _body.Mass = 100000;
        }
    }
}
