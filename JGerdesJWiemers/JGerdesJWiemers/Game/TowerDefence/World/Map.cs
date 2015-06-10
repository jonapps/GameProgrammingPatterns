using JGerdesJWiemers.Game.Engine.Graphics;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.World
{
    class Map : IRenderable
    {
        private Sprite[,] tiles;
        private Vector2i mapSize;
        private Vector2i tileSize; 

        public Map(int width, int height, int tileWidth = 64, int tileHeight = 32)
        {
            tiles = new Sprite[width, height];
            tileSize = new Vector2i(tileWidth, tileHeight);
            mapSize = new Vector2i(width, height);
            Texture tex = new Texture(@"Assets\Graphics\tiles\grassdirt.png");
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Sprite tile = new Sprite(tex);
                    tile.Position = MapToScreen(new Vector2f(x, y));
                    tiles[x, y] = tile;

                }
            }
        }

        public Vector2f MapToScreen(Vector2f mapPoint)
        {
            Vector2f result = new Vector2f();
            result.X = (mapPoint.X - mapPoint.Y) * (tileSize.X / 2f);
            result.Y = (mapPoint.X + mapPoint.Y) * (tileSize.Y / 2f);
            return result;
        }

        public Vector2f ScreenToMap(Vector2f screenPoint)
        {
            Vector2f result = new Vector2f();
            result.X = 0.5f * screenPoint.X - screenPoint.Y;
            result.Y = 0.5f * screenPoint.X - screenPoint.Y;
            return result;
        }

        public Vector2i GetTileIndexAt(Vector2f screenPoint)
        {
            Vector2f mapped = ScreenToMap(screenPoint);
            Vector2i result = new Vector2i();
            result.X = ((int)mapped.X) / tileSize.X;
            result.Y = ((int)mapped.Y) / tileSize.Y;
            return result;

        }
    
        public void Render(RenderTarget renderTarget, float extra)
        {
            for (int x = 0; x < mapSize.X; x++)
            {
                for (int y = 0; y < mapSize.Y; y++)
                {
                    renderTarget.Draw(tiles[x, y]);

                }
            }
        }
    }
}
