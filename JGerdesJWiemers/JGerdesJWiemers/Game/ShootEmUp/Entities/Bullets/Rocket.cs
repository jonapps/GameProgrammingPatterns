using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Entities.Bullets
{
    class Rocket : Bullet
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="direction"></param>
        /// <param name="rotation"></param>
        /// <param name="scale"></param>
        public Rocket(float x, float y, World w, Vector2 direction, float rotation, float scale)
            : base(x, y, w, AssetLoader.Instance.LoadTexture(AssetLoader.TEXTURE_SPACESHIP), direction, rotation, 0.5f, 1000f)
        {
            _blastRadius = 10;
            _blastStrength = 60;
            _speed = 100;
        }
    }
}
