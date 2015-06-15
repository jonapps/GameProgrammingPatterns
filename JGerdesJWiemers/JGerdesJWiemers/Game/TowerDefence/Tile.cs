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
       
        public Tile(float x, float y, float width, float height, Texture tex, int mapCenter)
        {
            _sprite = new Sprite(tex);
            _sprite.Position = Map.MapToScreen(x * width + mapCenter, y * height);
            _sprite.Origin = new Vector2f(width, 0);

            
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

        public Vector2f getCenter()
        {
            return new Vector2f();
        }
    }
}
