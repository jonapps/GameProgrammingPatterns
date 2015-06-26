using FarseerPhysics;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.EventSystem;
using JGerdesJWiemers.Game.Engine.EventSystem.Events;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Interfaces;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.TowerDefence.Logic;
using JGerdesJWiemers.Game.TowerDefence.Screens;
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
    class Tower : SpriteEntity
    {
        public static readonly String EVENT_BUILD = "tower.create";

        public class Def
        {
            public float Radius = 3;
            public int FireFrequency = 200;
            public float Damage = 1;
            public float BulletSpeed = 4;
            public Color Base;
            public Color TopActive;
            public Color TopWaiting;
            public Vector2 Position;

            public Def()
            {
                Base = new Color(63, 81, 181);
                TopActive = new Color(255, 152, 0);
                TopWaiting = new Color(255, 224, 178);
                Position = new Vector2();
            }
        }

        private Def _def;
        private long _lastFired = 0;
        private IEntityHolder _entityHolder;
        private AnimatedSprite _top;

        public Tower(World world, Def def, IEntityHolder holder)
            : base(world, AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_TOWER_BASE), 1, 0, 0, BodyType.Static)
        {
            TextureContainer tex = AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_TOWER_TOP);
            _sprite.Color = def.Base;
            _top = new AnimatedSprite(tex.Texture, tex.Width, tex.Height);
            _top.Origin = new Vector2f(tex.Width / 2f, tex.Height / 2f);
            _top.Color = def.TopWaiting;
            _body.Position = ConvertUnits.ToSimUnits(def.Position);
            _def = def;
            _entityHolder = holder;
            _CalcZ();
            _body.CollisionCategories = EntityCategory.Tower;
        }

        public override void Update()
        {
            base.Update();
            _top.Update();
            Entity destination = _entityHolder.GetEntities()
                                    .FindAll(e => (e.Position - _body.WorldCenter).LengthSquared() <= _def.Radius * _def.Radius && e is Enemy)
                                    .OrderBy(e => (e.Position - _body.WorldCenter).LengthSquared())
                                    .FirstOrDefault();

            if (destination != null)
            {
                _top.Color = _def.TopActive;
                Vector2 direction = destination.Position - this.Position;
                double angle = (float)(SMath.Atan2(direction.Y, direction.X) / SMath.PI) / 2 + 0.5f;
                int index = (int)((angle * 31)) + 18;
                if (index > 31)
                {
                    index -= 31;
                }
                _top.SetAnimation(new Animation(new int[] { index }, 1000, false));

                if (Game.ElapsedTime - _lastFired > _def.FireFrequency)
                {
                    //find a monster in radius

                    _lastFired = Game.ElapsedTime;
                    _Fire(destination);
                }
            }
            else
            {
                _top.Color = _def.TopWaiting;
            }
        }

        public override void PreDraw(float extra)
        {
            base.PreDraw(extra);
            _top.Position = Map.MapToScreen(_ConvertVector2ToVector2f(ConvertUnits.ToDisplayUnits(_body.WorldCenter) + new Vector2(-4, -8)));
        }

        public override void Draw(SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states)
        {
            base.Draw(target, states);
            target.Draw(_top, states);
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
