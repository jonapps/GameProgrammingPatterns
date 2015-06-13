using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Graphics;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence
{
    class Monster : SpriteEntity
    {
        private Map _map;
        public Monster(FarseerPhysics.Dynamics.World w, Map map)
            : base(w, new CircleTextureContainer(new Texture(@"Assets/Graphics/guy.png"), 39, 69, 16), 1)
        {
            _sprite.SetAnimation(new Animation(0, 7, 100, true, false));

            _map = map;
            
            //_sprite.Scale = new Vector2f(1, 1);
            //_body.Position = new Vector2(20, 20);
            _sprite.Origin = new Vector2f(39 / 2f, 59);
            _body.Position = new Vector2(2, 2);
            _body.LinearVelocity = new Vector2(1, 0);
        }

        public override void Update()
        {
            base.Update();
            Vector2 pos = getPositionInPixel();
            Tile tile = _map.GetTileAtMapPoint(pos.X, pos.Y);
            if (tile != null)
                tile.mark();
        }
    }
}
