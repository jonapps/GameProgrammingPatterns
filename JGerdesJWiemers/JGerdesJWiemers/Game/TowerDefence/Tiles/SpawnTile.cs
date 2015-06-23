using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.Tiles
{
    class SpawnTile : Tile
    {
        public SpawnTile(float x, float y, float width, float height, Texture tex, int mapCenter)
            : base(x, y, width, height, tex, mapCenter)
        {
            _sprite.Color = new Color(254, 192, 6);
        }

        public override TileType GetType()
        {
            return TileType.SpawnTile;
        }
    }
}
