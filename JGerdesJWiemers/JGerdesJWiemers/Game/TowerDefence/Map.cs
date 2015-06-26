using JGerdesJWiemers.Game.Engine.EventSystem;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Interfaces;
using JGerdesJWiemers.Game.Engine.Shapes;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.Engine.Utils.Helper;
using JGerdesJWiemers.Game.Engine.Utils.Helper.LevelAssets;
using JGerdesJWiemers.Game.TowerDefence.Entities;
using JGerdesJWiemers.Game.TowerDefence.Tiles;
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
        private Tile[,] _tiles;
        private List<Tile> _spawnTiles;
        private Vector2i _mapSize;
        private Vector2i _tileSize;

        private RenderTarget _converter;
        private View _view;

        public int MapOffsetX { get; set; }

        
        public Map(MapAsset asset)
        {
            _tiles = new Tile[asset.Width, asset.Height];
            _mapSize = new Vector2i(asset.Width, asset.Height);
            _tileSize = new Vector2i(asset.Tileheight, asset.Tileheight);
            _spawnTiles = new List<Tile>();
            _CreateMapByAsset(asset.Width, asset.Height, asset.Tileheight, asset.Tileheight, asset);



            EventStream.Instance.On(Enemy.EVENT_SPAWN, _OnEnemySpawn);
        }

        private void _OnEnemySpawn(Engine.EventSystem.Events.EngineEvent eventData)
        {
            
        }

        /// <summary>
        /// Creates the map by MapAsset information
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        /// <param name="asset"></param>
        private void _CreateMapByAsset(int width, int height, int tileWidth, int tileHeight, MapAsset asset)
        {
            List<TileImageAsset> imageAssets = asset.TileSets.FirstOrDefault(t => t.Name == "Underground").TileImages;
            int nextTex = 0;
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    _CreateTileByAsset(tileWidth, tileHeight, asset, imageAssets, nextTex++, y, x);
                }
            }
            
        }

        /// <summary>
        /// Creates a Tile at the next x / y pos 
        /// </summary>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        /// <param name="asset"></param>
        /// <param name="textures"></param>
        /// <param name="nextTex"></param>
        /// <param name="y"></param>
        /// <param name="x"></param>
        private void _CreateTileByAsset(int tileWidth, int tileHeight, MapAsset asset, List<TileImageAsset> imageAssets, int nextTex, int y, int x)
        {
            Texture tex = null;
            foreach (LayerAsset l in asset.Layers)
            {
                int nextNumber = l.Data[nextTex];
                
                if(nextNumber > 0)
                {
                    Tile t = null;
                    TileImageAsset tileimageasset = imageAssets.FirstOrDefault(res => res.Number == nextNumber);
                    tex = new Texture(AssetLoader.Instance.getTexture(tileimageasset.Image).Texture);
                    switch (l.Name)
                    {
                        case "Terrain":
                            t = new TerrainTile(x, y, tileWidth, tileHeight, tex, MapOffsetX, tileimageasset.Color);
                            break;
                        case "Road":
                            t = new RoadTile(x, y, tileWidth, tileHeight, tex, MapOffsetX, tileimageasset.Color);
                            break;
                        case "NoBuildArea":
                            t = new NoBuildTile(x, y, tileWidth, tileHeight, tex, MapOffsetX, tileimageasset.Color);
                            break;
                        case "EnemySpawn":
                            t = new SpawnTile(x, y, tileWidth, tileHeight, tex, MapOffsetX, tileimageasset.Color);
                            _spawnTiles.Add(t);
                            break;
                        case "Despawn":
                            t = new DespawnTile(x, y, tileWidth, tileHeight, tex, MapOffsetX, tileimageasset.Color);
                            break;
                    }
                    _tiles[x, y] = t;
                }
            }
        }

        /// <summary>
        /// Creates a radom map from a list of graphics
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        /// <param name="textures"></param>
        private void _CreateRandomMap(int width, int height, int tileWidth, int tileHeight, List<Texture> textures)
        {
            Texture tex;
            Random rand = new Random();
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int randomNUmber = rand.Next(0, textures.Count);
                    _tiles[x, y] = new TerrainTile(x, y, tileWidth, tileHeight, textures[randomNUmber], MapOffsetX, new Color());
                }
            }
        }

        
        /// <summary>
        /// maps screen corrds to map coords
        /// </summary>
        /// <param name="screenX"></param>
        /// <param name="screenY"></param>
        /// <returns></returns>
        public static Vector2f ScreenToMap(float screenX, float screenY)
        {
            Vector2f result = new Vector2f();
            result.X = 0.5f * screenX + screenY;
            result.Y = -0.5f * screenX + screenY;
            return result;
        }

        /// <summary>
        /// maps map coords to screen coords
        /// </summary>
        /// <param name="mapX"></param>
        /// <param name="mapY"></param>
        /// <returns></returns>
        public static Vector2f MapToScreen(float mapX, float mapY)
        {
            Vector2f result = new Vector2f();
            result.X = mapX - mapY;
            result.Y = 0.5f * mapX + 0.5f * mapY;
            return result;
        }

        /// <summary>
        /// Returns all spawnpositions
        /// </summary>
        /// <returns></returns>
        public List<Tile> GetSpawnTiles()
        {
            return _spawnTiles;
        }

        public Vector2i GetTileIndexAtScreenPoint(float screenX, float screenY)
        {
            Vector2f mapped = ScreenToMap(screenX, screenY);
            return GetTileIndexAtMapPoint(mapped.X, mapped.Y);

        }


        public Vector2i GetTileIndexAtMapPoint(float mapX, float mapY)
        {
           
            Vector2i result = new Vector2i();
            result.X = (int)(mapX / _tileSize.X);
            result.Y = (int)(mapY / _tileSize.Y);
            return result;

        }

        public Tile GetTileAtScreenPoint(float screenX, float screenY)
        {
            Vector2i index = GetTileIndexAtScreenPoint(screenX, screenY);
            if (index.X >= 0 && index.X < _mapSize.X && index.Y >= 0 && index.Y < _mapSize.Y)
                return _tiles[index.X, index.Y];
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
            if (index.X >= 0 && index.X < _mapSize.X && index.Y >= 0 && index.Y < _mapSize.Y)
                return _tiles[index.X, index.Y];
            else
                return null;
        }

        public Tile GetTileByIndex(int x, int y)
        {
            if (x >= 0 && x <_mapSize.X && y >= 0 && y < _mapSize.Y)
                return _tiles[x, y];
            return null;
        }
   

        public void Update()
        {
            for (int x = 0; x < _mapSize.X; x++)
            {
                for (int y = 0; y < _mapSize.Y; y++)
                {
                    _tiles[x, y].Update();
                }
            }
        }

        public void PastUpdate()
        {
            
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            for (int x = 0; x < _mapSize.X; x++)
            {
                for (int y = 0; y < _mapSize.Y; y++)
                {
                    target.Draw(_tiles[x, y], states);
                }
            }
        }


        public void PreDraw(float extra)
        {
            for (int x = 0; x < _mapSize.X; x++)
            {
                for (int y = 0; y < _mapSize.Y; y++)
                {
                    _tiles[x, y].PreDraw(extra);
                }
            }
        }

        public static Vector2f MapToScreen(Vector2f pos)
        {
            return MapToScreen(pos.X, pos.Y);
        }
    }
}
