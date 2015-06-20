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
    class Tile : IDrawable
    {
        Sprite _sprite;
        private bool _isRoad = false;

        public bool IsRoad { get { return _isRoad; } }

        public Tile(float x, float y, float width, float height, Texture tex, int mapCenter, bool isRoad)
        {
            _sprite = new Sprite(tex);
            _sprite.Position = Map.MapToScreen(x * width + mapCenter, y * height);
            _sprite.Origin = new Vector2f(width, 0);
            _isRoad = isRoad;
            
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
            if (_isRoad)
            {
                _sprite.Color = new Color(0, 255, 0);
            }
            else
            {
                _sprite.Color = new Color(255, 0, 0);
            }
        }

        public Vector2f getCenter()
        {
            return new Vector2f();
        }
    }
}
