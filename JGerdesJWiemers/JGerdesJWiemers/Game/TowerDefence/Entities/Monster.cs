using FarseerPhysics;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.TowerDefence.Logic.AI;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMath = System.Math;

namespace JGerdesJWiemers.Game.TowerDefence.Entities
{
    class Monster : SpriteEntity
    {
        public static readonly String EVENT_SPAWN = "monster.spawn";
        private static readonly float STOPPING_DISTANCE = 0.1f;

        private FollowRoadAI _ai;
        private Vector2 _destination;
        private float _speed = 2;

        public Monster(FarseerPhysics.Dynamics.World w, float x, float y, FollowRoadAI ai)
            : base(w, AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_GUY), 1)
        {
            _sprite.SetAnimation(new Animation(0, 7, 100, true, false));
            _body.Position = ConvertUnits.ToSimUnits(x, y);
            _destination = ConvertUnits.ToSimUnits(x + 300, y);

            _ai = ai;
            _ai.OnDestinationChanged += OnDestinationChanged;

        }

        void OnDestinationChanged(Tile destination)
        {
            _destination = ConvertUnits.ToSimUnits(destination.getCenter());
            destination.mark();
        }

        public override void Update()
        {
            base.Update();
            _ai.Update(_body);

            Vector2 direction = _destination - _body.WorldCenter;
            float distance = direction.Length();
            if (distance > STOPPING_DISTANCE)
            {
                direction /= distance;
                _body.LinearVelocity = direction * _speed;
            }
            else
            {
                _body.LinearVelocity = new Vector2(0, 0);
            }
            
        }
    }
}
