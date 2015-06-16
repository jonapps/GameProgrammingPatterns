using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Interfaces;
using JGerdesJWiemers.Game.Engine.Shapes;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence
{
    class Map : IDrawable 
    {
        private Tile[,] tiles;
        private Vector2i mapSize;
        private Vector2i tileSize;
        public int MapOffsetX { get; set; }

        public Map(int width, int height, int tileSize = 32)
            : this(width, height, tileSize, tileSize) { }
        public Map(int width, int height, int tileWidth, int tileHeight)
        {
            MapOffsetX = 0;
            tiles = new Tile[width, height];
            mapSize = new Vector2i(width, height);
            tileSize = new Vector2i(tileWidth, tileHeight);
            Texture tex1 = new Texture(@"Assets/Graphics/tiles/grass.png");
            Texture tex2 = new Texture(@"Assets/Graphics/tiles/grassdirt.png");
            Texture tex;
            Random rand = new Random();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if(rand.Next(2) == 0){
                        tex = tex2;
                    }
                    else
                    {
                        tex = tex1;
                    }
                    tiles[x, y] = new Tile(x, y, tileWidth, tileHeight, tex, MapOffsetX);
                }
            }
        }

        public static Vector2f ScreenToMap(float screenX, float screenY)
        {
            Vector2f result = new Vector2f();
            result.X = 0.5f * screenX + screenY;
            result.Y = -0.5f * screenX + screenY;
            return result;
        }

        public static Vector2f MapToScreen(float mapX, float mapY)
        {
            Vector2f result = new Vector2f();
            result.X = mapX - mapY;
            result.Y = 0.5f * mapX + 0.5f * mapY;
            return result;
        }

        public Vector2i GetTileIndexAtScreenPoint(float screenX, float screenY)
        {
            Vector2f mapped = ScreenToMap(screenX, screenY);
            return GetTileIndexAtMapPoint(mapped.X, mapped.Y);

        }


        public Vector2i GetTileIndexAtMapPoint(float mapX, float mapY)
        {
           
            Vector2i result = new Vector2i();
            result.X = ((int)mapX) / tileSize.X;
            result.Y = ((int)mapY) / tileSize.Y;
            float x = SFML.Window.Mouse.GetPosition().X;
            return result;

        }

        public Tile GetTileAtScreenPoint(float screenX, float screenY)
        {
            Vector2i index = GetTileIndexAtScreenPoint(screenX, screenY);
            if (index.X >= 0 && index.X < mapSize.X && index.Y >= 0 && index.Y < mapSize.Y)
                return tiles[index.X, index.Y];
            else
                return null;
        }

        public Tile GetTileAtScreenPoint(Vector2f screenPoint)
        {
            return GetTileAtScreenPoint(screenPoint.X, screenPoint.Y);
        }

        public Tile GetTileAtScreenPoint(Vector2i screenPoint)
        {
            return GetTileAtScreenPoint(screenPoint.X, screenPoint.Y);
        }

        public Tile GetTileAtMapPoint(float mapX, float mapY)
        {
            Vector2i index = GetTileIndexAtMapPoint(mapX, mapY);
            if (index.X >= 0 && index.X < mapSize.X && index.Y >= 0 && index.Y < mapSize.Y)
                return tiles[index.X, index.Y];
            else
                return null;
        }
   

        public void Update()
        {
            
        }

        public void PastUpdate()
        {
            
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            for (int x = 0; x < mapSize.X; x++)
            {
                for (int y = 0; y < mapSize.Y; y++)
                {
                    target.Draw(tiles[x, y], states);
                }
            }
        }


        public void PreDraw(float extra)
        {
            
        }
    }
}
