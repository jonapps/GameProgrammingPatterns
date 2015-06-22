using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.Tiles
{
    class TerrainTile : Tile
    {
        public TerrainTile(float x, float y, float width, float height, Texture tex, int mapCenter)
            : base(x, y, width, height, tex, mapCenter)
        {

        }


        public override TileType GetType()
        {
            return TileType.BuildTile;
        }
    }
}
