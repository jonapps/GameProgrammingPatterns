using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.ShootEmUp.Entities;
using Microsoft.Xna.Framework;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Weapons
{
    class RocketLauncher : Weapon
    {

        public RocketLauncher()
        {
            _toShoot = 200;
            _clock.Restart();
        }

        public override Entity Shoot(float x, float y, World w, Vector2 direction, float rotation)
        {
            if (_clock.ElapsedTime.AsMilliseconds() > _toShoot)
            {
                _clock.Restart();
                return new Bullet(x, y, w, direction, rotation);
            }
            return null;
        }
    }
}
