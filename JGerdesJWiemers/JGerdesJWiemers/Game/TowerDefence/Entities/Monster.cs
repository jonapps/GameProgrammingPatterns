using FarseerPhysics;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.TowerDefence.Logic.AI;
using JGerdesJWiemers.Game.TowerDefence.Tiles;
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
        private float _speed = 1.5f;

        private Animation _right;
        private Animation _left;
        private Animation _up;
        private Animation _down;

        public Monster(FarseerPhysics.Dynamics.World w, float x, float y, FollowRoadAI ai)
            : base(w, AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_GUY), 0.75f)
        {
            _right = new Animation(2 * 8, 3 * 8 - 1, 100, true, false);
            _left = new Animation(1 * 8, 2 * 8 - 1, 100, true, false);
            _up = new Animation(0 * 8, 1 * 8 - 1, 100, true, false);
            _down = new Animation(3 * 8, 4 * 8 - 1, 100, true, false);
            _sprite.SetAnimation(_right);
            _body.Position = ConvertUnits.ToSimUnits(x, y);
            _destination = _body.Position;

            _ai = ai;
            _ai.OnDestinationChanged += OnDestinationChanged;

        }

        void OnDestinationChanged(Tile destination)
        {
            _destination = ConvertUnits.ToSimUnits(destination.getCenter());
            Vector2 direction = _destination - _body.WorldCenter;
            float distance = direction.Length();
            direction /= distance;

            if (direction.X > STOPPING_DISTANCE)
            {
                _sprite.SetAnimation(_right);
            }
            if (direction.X < -STOPPING_DISTANCE)
            {
                _sprite.SetAnimation(_left);
            }

            if (direction.Y > STOPPING_DISTANCE)
            {
                _sprite.SetAnimation(_down);
            }
            if (direction.Y < -STOPPING_DISTANCE)
            {
                _sprite.SetAnimation(_up);
            }
        }

        public override void Update()
        {
            base.Update();
           

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
                _ai.Update(_body);
            }

            

            //if (direction.X < STOPPING_DISTANCE)
            //{
            //    if (direction.Y < STOPPING_DISTANCE)
            //    {
            //        _ai.Update(_body);
            //    }
            //    else
            //    {
            //        _body.LinearVelocity = new Vector2(0, SMath.Sign(direction.Y) * _speed);
            //    }
                
            //}
            //else
            //{
            //    _body.LinearVelocity = new Vector2(SMath.Sign(direction.X) * _speed, 0);
            //}
            
        }
    }
}
