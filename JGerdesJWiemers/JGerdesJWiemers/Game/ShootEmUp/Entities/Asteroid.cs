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
        public Asteroid(World world, float x, float y, string textureName, float scale = 1, float xSpeed = 0, float ySpeed = 0, float rotSpeed = 0)
            : base(world, AssetLoader.Instance.getTexture(textureName), scale)
        {
            _body.LinearVelocity = new Vector2(xSpeed, ySpeed);
            _body.ApplyAngularImpulse(rotSpeed);
            _body.Mass = 10;
        }
    }
}
