using FarseerPhysics;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Interfaces;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.TowerDefence.Logic;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.Entities
{
    class Tower : SpriteEntity
    {

        public class Def
        {
            public float Radius = 100;
            public int FireFrequency = 1;
            public float Damage = 1;
        }

        private Def _def;
        private long _lastFired = 0;
        private IMapRadar _radar;

        public Tower(World world, float x, float y, Def def, IMapRadar radar)
            : base(world, AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_TOWER), 1, 0, 0, BodyType.Static)
        {
            _body.Position = ConvertUnits.ToSimUnits(x, y);
            _def = def;
            _radar = radar;
        }

        public override void Update()
        {
            base.Update();

            if (Game.ElapsedTime - _lastFired > 1000f / _def.FireFrequency)
            {

                Entity destination = _radar.FindEntitiesAround(ConvertUnits.ToDisplayUnits(_body.WorldCenter), _def.Radius).Find(e => e is Monster);

                if (destination != null)
                {
                    _lastFired = Game.ElapsedTime;
                    _Fire(destination);
                }
                

            }
        }

        private void _Fire(Entity destination)
        {
            Console.WriteLine("firing on " + destination.ToString());
        }


       
    }
}
