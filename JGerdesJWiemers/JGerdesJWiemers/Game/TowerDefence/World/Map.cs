using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Shapes;
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
        private PolygonShape[,] tiles;
        private Vector2i mapSize;
        private Vector2i tileSize; 

        public Map(int width, int height, int tileSize = 32)
            : this(width, height, tileSize, tileSize) { }
        public Map(int width, int height, int tileWidth, int tileHeight)
        {

            tiles = new PolygonShape[width, height];
            mapSize = new Vector2i(width, height);
            tileSize = new Vector2i(tileWidth, tileHeight);

            List<Vector2f> points;
            Color fill = new Color(255, 255, 255, 100);
            Color bound = new Color(255, 255, 255);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    points = new List<Vector2f>();
                    points.Add(MapToScreen(new Vector2f(x * tileWidth, y * tileHeight)));
                    points.Add(MapToScreen(new Vector2f((x+1) * tileWidth, y * tileHeight)));
                    points.Add(MapToScreen(new Vector2f((x+1) * tileWidth, (y+1) * tileHeight)));
                    points.Add(MapToScreen(new Vector2f(x * tileWidth, (y+1) * tileHeight)));
                    PolygonShape s = new PolygonShape(points);
                    s.FillColor = fill;
                    s.OutlineColor = bound;
                    s.OutlineThickness = 0.5f;
                    tiles[x, y] = s;

                }
            }
        }

        public Vector2f ScreenToMap(Vector2f mapPoint)
        {
            Vector2f result = new Vector2f();
            result.X = 0.5f * mapPoint.X + mapPoint.Y;
            result.Y = -0.5f * mapPoint.X + mapPoint.Y;
            return result;
        }

        public Vector2f MapToScreen(Vector2f screenPoint)
        {
            Vector2f result = new Vector2f();
            result.X = screenPoint.X - screenPoint.Y;
            result.Y = 0.5f * screenPoint.X + 0.5f * screenPoint.Y;
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

        public Shape GetTileAt(Vector2f screenPoint)
        {
            Vector2i index = GetTileIndexAt(screenPoint);
            if (index.X >= 0 && index.X < mapSize.X && index.Y >= 0 && index.Y < mapSize.Y)
                return tiles[index.X, index.Y];
            else
                return null;
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
