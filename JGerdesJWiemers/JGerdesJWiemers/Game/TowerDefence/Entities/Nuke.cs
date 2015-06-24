using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.EventSystem.Events;
using JGerdesJWiemers.Game.Engine.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMath = System.Math;

namespace JGerdesJWiemers.Game.TowerDefence.Entities
{
    class Nuke : SpriteEntity
    {
        public static readonly String EVENT_SPAWN = "nuke.spawn";
        private long _timeToLive = 1000;
        private long _startTime = 0;
        public class Def
        {
            public Vector2 Position;
            public Vector2 Destination;
            public float Speed;
        }


        public Nuke(World world, Def def)
            :base(world, AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_NUKE))
        {
            _body.Position = def.Position;
            _body.FixedRotation = true;
            Vector2 direction = (def.Destination - _body.WorldCenter);
            direction.Normalize();
            _body.LinearVelocity = direction * def.Speed;
            _sprite.Rotation =  (float) (SMath.Atan2(direction.Y, direction.X) * (180 / SMath.PI));
            _startTime = Game.ElapsedTime;
        }

        public override void Update()
        {
            base.Update();
            if ((Game.ElapsedTime - _startTime) > _timeToLive)
            {
                _deleteMe = true;
            }
        }
 
    }
}
