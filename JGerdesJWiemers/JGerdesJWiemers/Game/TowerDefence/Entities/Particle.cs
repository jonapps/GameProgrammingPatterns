using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Entities;
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

        public class Def
        {
            public Vector2 Position { get; set; }
            public Vector2 Destination { get; set; }
            public float Speed { get; set; }
            public Color Color { get; set; }


            public int Energy { get; set; }
        }

        public Particle(World world, Def def)
            :base(world, AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_BULLET))
        {
            Random rand = new Random();
            _moveTime = rand.Next(50, 150);
            _sprite.Scale = new Vector2f(0.4f, 0.4f);
            _sprite.Color = def.Color;
            _body.Position = def.Position;
            _body.CollisionCategories = EntityCategory.Particle;
            Vector2 direction = new Vector2((float)rand.NextDouble(), (float)rand.NextDouble());
            direction.Normalize();

            _body.LinearVelocity = direction * (_moveTime / 40);
            _started = Game.ElapsedTime;
        }

        public override void Update()
        {
            base.Update();
            if (Game.ElapsedTime - _started > _moveTime)
            {
                _body.LinearVelocity = new Vector2(0, 0);
                _body.Enabled = false;
            }
        }

        
    }
}
