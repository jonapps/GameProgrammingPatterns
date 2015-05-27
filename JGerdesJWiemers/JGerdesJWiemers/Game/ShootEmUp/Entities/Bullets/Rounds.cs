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
    class Rounds : Bullet
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
        public Rounds(float x, float y, World w, Vector2 direction, float rotation, float scale)
            : base(x, y, w, AssetLoader.Instance.LoadTexture(AssetLoader.GATLINGUN_BULLET), direction, rotation, scale)
        {
            _blastRadius = 1;
            _blastStrength = 1;
            _speed = 1000;
            base.Start();
        }
    }
}
