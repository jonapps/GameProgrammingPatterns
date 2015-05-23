using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using Microsoft.Xna.Framework;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Weapons
{
    abstract class Weapon
    {
        protected Clock _clock;
        protected float _toShoot;
        protected List<Entity> _bullets;

        public Weapon()
        {
            _clock = new Clock();
            _bullets = new List<Entity>();
        }

        public abstract List<Entity> Shoot(float x, float y, World w, Vector2 direction, float rotation);
    }
}
