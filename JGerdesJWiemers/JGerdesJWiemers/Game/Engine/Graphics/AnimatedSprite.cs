using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Graphics
{
    class AnimatedSprite : Sprite
    {
        private int _tileWidth;
        private int _tileHeight;
        private int _rows;
        private int _columns;

        private int _currentIndex;
        private int _timePassed;
        private int _currentDuration;

        public AnimatedSprite(Texture tex, int tileWidth, int tileHeight)
            : base(tex)
        {
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _columns = (int) tex.Size.X / _tileWidth;
            _rows = (int) tex.Size.Y / _tileHeight;

            _timePassed = 0;
            _currentDuration = 1000;

            TextureRect = new IntRect(0, 0, _tileWidth, _tileHeight);
        }

        public new void Draw(RenderTarget renderTarget, RenderStates renderStates, float exta)
        {
            _timePassed += Game.ElapsedTime;
            if (_timePassed >= _currentDuration)
            {

            }
            base.Draw(renderTarget, renderStates);

        }
    }
}
