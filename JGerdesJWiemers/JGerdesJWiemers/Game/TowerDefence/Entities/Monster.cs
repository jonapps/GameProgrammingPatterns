﻿using FarseerPhysics;
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
        private float _speed = 0.5f;
        private Sprite _shadow;

        public Monster(FarseerPhysics.Dynamics.World w, float x, float y, FollowRoadAI ai)
            : base(w, AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_GUY), 0.75f)
        {
 
            _body.Position = ConvertUnits.ToSimUnits(x, y);
            _destination = _body.Position;
            _sprite.Color = new Color(102, 57, 182);
            TextureContainer tex =  AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_SHADOW);
            _shadow = new Sprite(tex.Texture);
            _shadow.Origin = new Vector2f(tex.Width / 2f, tex.Height / 2f);
            _shadow.Color = new Color(0, 0, 0, 140);
            _ai = ai;
            _ai.OnDestinationChanged += OnDestinationChanged;
            _ai.OnOnDespawn += _OnOnDespawn;
            _ai.OnTileEnter += _OnTileEnter;
            _ai.OnTileLeft += _OnTileLeft;

        }

        void _OnTileLeft(Tile t)
        {
            t.RemoveEntity(this);
        }

        void _OnTileEnter(Tile t)
        {
            t.AddEntity(this);
        }

        private void _OnOnDespawn(Tile destination)
        {
            _deleteMe = true;
        }


        void OnDestinationChanged(Tile destination)
        {
            _destination = ConvertUnits.ToSimUnits(destination.getCenter());
          
        }

        public override void PreDraw(float extra)
        {
            base.PreDraw(extra);
            float sine = (float)SMath.Sin(Game.ElapsedTime / 200f);
            _sprite.Position += new Vector2f(0,  sine * 6 - 12);
            float scale = (sine * 0.5f + 0.5f) * 0.4f + 0.4f;
            _shadow.Scale = new Vector2f(scale, scale);
            _shadow.Position = Map.MapToScreen(_ConvertVector2ToVector2f(ConvertUnits.ToDisplayUnits(_body.WorldCenter)));
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_shadow, states);
            base.Draw(target, states);
        }

        public override void PastUpdate()
        {
            base.PastUpdate();
            _CalcZ(); 
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
            }
            _ai.Update(_body);

        }
    }
}
