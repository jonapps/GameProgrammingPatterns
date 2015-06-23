using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.Tiles
{
    class RoadTile : Tile
    {
        public RoadTile(float x, float y, float width, float height, Texture tex, int mapCenter)
            : base(x,y,width,height,tex,mapCenter)
        {
            _sprite.Color = new Color(3, 169, 244);
        }

        public override TileType GetType()
        {
            return TileType.RoadTile;
        }

    }
}
