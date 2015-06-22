using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Interfaces;
using JGerdesJWiemers.Game.Engine.Shapes;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.Tiles
{
    enum TileType
    {
        RoadTile,
        NoBuildTile,
        BuildTile,
        SpawnTile
    }

    abstract class Tile : IDrawable
    {
        Sprite _sprite;
        private bool _isRoad = false;
        private Vector2 _size;
        private Vector2 _position;
        private Entity _occupier;


        public Tile(float x, float y, float width, float height, Texture tex, int mapCenter)
        {
            _sprite = new Sprite(tex);
            _sprite.Position = Map.MapToScreen(x * width + mapCenter, y * height);
            _sprite.Origin = new Vector2f(width, 0);
            _size = new Vector2(width, height);
            _position = new Vector2(x * width, y * height);     
        }


        public bool IsRoad { get { return _isRoad; } }

        public bool IsOccupied { get { return _occupier != null; } }

        public Entity Occupier
        {
            get
            {
                return _occupier;
            }

            set
            {
                if (_occupier == null && value != null)
                {
                    _occupier = null;
                }
            }
        }

        public void Update()
        {
        }

        public void PastUpdate()
        {
        }

        public void PreDraw(float extra)
        {
        }

        public void Draw(SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states)
        {
            target.Draw(_sprite, states);
        }

        public void mark()
        {
            _sprite.Color = new Color(255, 0, 0);
        }

        public Vector2 getCenter()
        {
            return _position + 0.5f * _size;
        }

        public abstract TileType GetType();
    }
}
