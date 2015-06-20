using FarseerPhysics;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Utils;
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
        public Tower(World world, float x, float y)
            : base(world, AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_TOWER))
        {
            _body.Position = ConvertUnits.ToSimUnits(x, y);
        }


       
    }
}
