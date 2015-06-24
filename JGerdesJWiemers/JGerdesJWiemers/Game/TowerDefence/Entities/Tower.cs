using FarseerPhysics;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.EventSystem;
using JGerdesJWiemers.Game.Engine.EventSystem.Events;
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
            public float Radius = 3;
            public int FireFrequency = 200;
            public float Damage = 1;
            public float BulletSpeed = 4;
        }

        private Def _def;
        private long _lastFired = 0;
        private IEntityHolder _entityHolder;

        public Tower(World world, float x, float y, Def def, IEntityHolder holder)
            : base(world, AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_TOWER), 1, 0, 0, BodyType.Static)
        {
            _body.Position = ConvertUnits.ToSimUnits(x, y);
            _def = def;
            _entityHolder = holder;
            _CalcZ(); 
        }

        public override void Update()
        {
            base.Update();

            if ((Game.ElapsedTime - _lastFired) > _def.FireFrequency)
            {
                //find a monster in radius
                Entity destination = _entityHolder.GetEntities().Find(e => (e.Position - _body.WorldCenter).LengthSquared() <= _def.Radius * _def.Radius && e is Monster);

                if (destination != null)
                {
                    _lastFired = Game.ElapsedTime;
                    _Fire(destination);
                }
                

            }
        }

        private void _Fire(Entity destination)
        {
            Nuke.Def data = new Nuke.Def
            {
                Destination = destination.Position,
                Position = _body.WorldCenter,
                Speed = _def.BulletSpeed
            };

            EventStream.Instance.Emit(Nuke.EVENT_SPAWN, new EngineEvent(data));
        }


       
    }
}
