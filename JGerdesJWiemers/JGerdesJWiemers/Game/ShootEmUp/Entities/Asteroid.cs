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
            : base(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_ASTEROID3), 128, 76)
        {
            Vertices points = new Vertices();
            points.Add(ConvertUnits.ToSimUnits(scale * 12.6f, scale * 18.19999f));
            points.Add(ConvertUnits.ToSimUnits(scale * 26.19999f, scale * 12.2f));
            points.Add(ConvertUnits.ToSimUnits(scale * 56.59998f, scale * 3.599999f));
            points.Add(ConvertUnits.ToSimUnits(scale * 73.39997f, scale * 3.799999f));
            points.Add(ConvertUnits.ToSimUnits(scale * 95.59997f, scale * 14.39999f));
            points.Add(ConvertUnits.ToSimUnits(scale * 118f, scale * 9.799996f));
            points.Add(ConvertUnits.ToSimUnits(scale * 133f, scale * 10.8f));
            points.Add(ConvertUnits.ToSimUnits(scale * 141f, scale * 19.79999f));
            points.Add(ConvertUnits.ToSimUnits(scale * 135.3999f, scale * 34.39999f));
            points.Add(ConvertUnits.ToSimUnits(scale * 130.3999f, scale * 52.19998f));
            points.Add(ConvertUnits.ToSimUnits(scale * 88.59997f, scale * 62.59998f));
            points.Add(ConvertUnits.ToSimUnits(scale * 83.19997f, scale * 74.79997f));
            points.Add(ConvertUnits.ToSimUnits(scale * 67.19997f, scale * 82.79997f));
            points.Add(ConvertUnits.ToSimUnits(scale * 46.19998f, scale * 82.99997f));
            points.Add(ConvertUnits.ToSimUnits(scale * 35.19999f, scale * 76.59998f));
            points.Add(ConvertUnits.ToSimUnits(scale * 12.2f, scale * 55.99998f));
            points.Add(ConvertUnits.ToSimUnits(scale * 3.799999f, scale * 32.19999f));


            _Create(x, y, points, world);

            _sprite.Scale = new Vector2f(_sprite.Scale.X * scale, _sprite.Scale.Y * scale);

            _body.LinearVelocity = new Vector2(xSpeed, ySpeed);
            _body.ApplyAngularImpulse(rotSpeed);
            _body.Mass = 10;
        }
    }
}
