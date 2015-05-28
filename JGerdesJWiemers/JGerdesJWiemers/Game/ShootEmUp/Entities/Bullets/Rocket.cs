using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Audio;
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
        public static String ROCKET_EXPLODE = "ROCKET_EXPLODE";

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
            : base(x, y, w, AssetLoader.Instance.LoadTexture(AssetLoader.TEXTURE_MISSILE), direction, rotation, 0.8f)
        {
            AudioManager.Instance.AddSound(ROCKET_EXPLODE, "Assets/Audio/ROCKET_EXPLODE.wav");
            _blastRadius = 10;
            _blastStrength = 60;
            _speed = 100;
            base.Start();
        }

        protected override void _Blast()
        {
            base._Blast();
            AudioManager.Instance.Play(ROCKET_EXPLODE);
            EntityFactory.Instance.Spawn(new Explosion.ExplosionDef(_body.Position.X, _body.Position.Y, 0, 0, 1.2f, 0));
        }
    }
}
