using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Audio;
using JGerdesJWiemers.Game.ShootEmUp.Entities;
using JGerdesJWiemers.Game.ShootEmUp.Entities.Bullets;
using JGerdesJWiemers.Game.ShootEmUp.Logic;
using Microsoft.Xna.Framework;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Weapons
{
    class  DoubleGatlinGun : GatlinGun
    {
        public DoubleGatlinGun()
        {
            AudioManager.Instance.AddSound(GATLINGUN_SHOOT, "Assets/Audio/GATLINGUN_SHOOT.wav");
            _toShoot = 100;
            _clock.Restart();
        }

        public override List<Entity> Shoot(float x, float y, World w, Vector2 direction, float rotation)
        {
            _bullets.Clear();
            if (_clock.ElapsedTime.AsMilliseconds() > _toShoot)
            {
                if (GameManager.Instance.ReduceRounds(2) == 2)
                {
                    AudioManager.Instance.Play(GATLINGUN_SHOOT);
                    Vector2 directionNormal = new Vector2(direction.Y * -1, direction.X);
                    float x1 = (directionNormal * 2).X + x;
                    float y1 = (directionNormal * 2).Y + y;
                    float x2 = (directionNormal * -2).X + x;
                    float y2 = (directionNormal * -2).Y + y;
                    _clock.Restart();
                    _bullets.Add(new Rounds(x1, y1, w, direction, rotation, 0.5f));
                    _bullets.Add(new Rounds(x2, y2, w, direction, rotation, 0.5f));
                }
            }
            return _bullets;
        }
    }
}
