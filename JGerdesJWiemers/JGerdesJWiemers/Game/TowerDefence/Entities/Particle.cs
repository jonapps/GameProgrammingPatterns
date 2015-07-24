﻿using FarseerPhysics;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.EventSystem;
using JGerdesJWiemers.Game.Engine.EventSystem.Events;
using JGerdesJWiemers.Game.Engine.Interfaces;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.TowerDefence.Screens;
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
    class Particle : SpriteEntity
    {
        public static readonly string EVENT_SPAWN = "particle.spawn";
        private int _moveTime;
        private long _started;
        private int _energy;
        private ICoordsConverter _converter;
        private Vector2 _destination;

        private long _layTime = 5000;
        private long _layStarted;
        private bool _toDelivery = true;
        private bool _onDelivery = false;

        public class Def
        {
            public Vector2 Position { get; set; }
            public Vector2 Destination { get; set; }
            public float Speed { get; set; }
            public Color Color { get; set; }
            public int Energy { get; set; }
        }

        public Particle(World world, Def def, ICoordsConverter converter)
            :base(world, AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_BULLET))
        {
            _converter = converter;
            Random rand = new Random();
            _moveTime = rand.Next(50, 150);
            _sprite.Scale = new Vector2f(0.4f, 0.4f);
            _sprite.Color = def.Color;
            _body.Position = def.Position;
            _energy = def.Energy;
            Vector2 direction = new Vector2((float)rand.NextDouble(), (float)rand.NextDouble());
            direction.Normalize();
            _body.CollisionCategories = EntityCategory.Particle;
            _body.CollidesWith = 0;
            _body.LinearVelocity = direction * (_moveTime / 40);
            _started = Game.ElapsedTime;
            _body.Enabled = false;

        }

        public override void Update()
        {
            base.Update();
            if ((Game.ElapsedTime - _started > _moveTime) && (!_onDelivery))
            {
                if (_toDelivery)
                {
                    _body.Enabled = false;
                    _layStarted = Game.ElapsedTime;
                    _toDelivery = false;
                }
                
                if ((Game.ElapsedTime - _layStarted > _layTime) && (!_onDelivery))
                {
                    Vector2f pos = _converter.MapPixelToCoords(new Vector2i(0, 0));
                    Vector2 dest = ConvertUnits.ToSimUnits(pos.ToVector2());
                    Collect(dest);
                }
            }

            

            if (_onDelivery)
            {
                if ((_body.Position - _destination).Length() < 0.2)
                {
                    _body.Enabled = false;
                    _deleteMe = true;
                }

                if (_body.Position.X < - 15 || _body.Position.Y < - 15)
                {
                    _body.Enabled = false;
                    _deleteMe = true;
                }
            }
        }


        //(x2 - x1, y2 - y1)
        public void Collect(Vector2 dest)
        {
            EventStream.Instance.Emit(Enemy.EVENT_LOST_ENERGY, new EngineEvent(_energy));
            _body.Enabled = true;
            Vector2 pos = _body.Position;
            Vector2 direction = new Vector2();
            direction.X = dest.X - pos.X;
            direction.Y = dest.Y - pos.Y;
            direction.Normalize();
            direction *= .5f;
            _body.ApplyLinearImpulse(direction);
            _destination = dest;
            _onDelivery = true;
        }
    }
}
