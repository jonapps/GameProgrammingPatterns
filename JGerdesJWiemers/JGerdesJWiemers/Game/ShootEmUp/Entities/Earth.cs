using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Utils;
using Microsoft.Xna.Framework;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Entities
{
    class Earth : SpriteCircleEntity
    {

        private Animation _rotateAnimation;

        public Earth(float x, float y, World world, float radius, float scale = 1) :
            base(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_EARTH), 128, 128, x, y, radius, world)
        {
            _rotateAnimation = new Animation(0, 31, 1000, true, false);
            _sprite.SetAnimation(_rotateAnimation);
        }
    }
}
