using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.ShootEmUp.Entities;
using JGerdesJWiemers.Game.ShootEmUp.Entities.Bullets;
using Microsoft.Xna.Framework;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Weapons
{
    class GatlinGun : Weapon
    {

        public GatlinGun()
        {
            _toShoot = 100;
            _clock.Restart();
        }

        public override List<Entity> Shoot(float x, float y, World w, Vector2 direction, float rotation)
        {

            _bullets.Clear();
            if (_clock.ElapsedTime.AsMilliseconds() > _toShoot)
            {
                _clock.Restart();
                _bullets.Add(new Rounds(x, y, w, direction, rotation, 0.5f));
            }
            return _bullets;
        }
    }
}
