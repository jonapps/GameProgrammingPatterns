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
        public struct Animation
        {
            public int[] Frames;
            public bool Loop;
            public bool Pingpong;
            public int Duration;
        }

        private int _tileWidth;
        private int _tileHeight;
        private int _rows;
        private int _columns;

        private Queue<Animation> _animationQueue;

        private int _currentIndex;
        private int _timePassed;

        public AnimatedSprite(Texture tex, int tileWidth, int tileHeight)
            : base(tex)
        {
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _rows = (int) tex.Size.X / _tileWidth;
            _columns = (int) tex.Size.Y / _tileHeight;

            _timePassed = 0;

            _animationQueue = new Queue<Animation>();

            TextureRect = new IntRect(0, 0, _tileWidth, _tileHeight);

            Animation defaultAnimation = new Animation();
            defaultAnimation.Frames = new int[] { 0 };
            defaultAnimation.Loop = false;
            defaultAnimation.Pingpong = false;
            defaultAnimation.Duration = 1;

            _animationQueue.Enqueue(defaultAnimation);
        } 

        public new void Draw(RenderTarget renderTarget, RenderStates renderStates)
        {
            _timePassed += Game.ElapsedTime;
            if (_timePassed >= _animationQueue.Peek().Duration)
            {
                _NextFrame();
                _timePassed = 0;
            }
            base.Draw(renderTarget, renderStates);

        }


        public void SetAnimation(Animation animation)
        {
            _animationQueue.Clear();
            _animationQueue.Enqueue(animation);
            _currentIndex = 0;
        }

        public void EnqueueAnimation(Animation animation)
        {
            _animationQueue.Enqueue(animation);
        }

        private void _NextFrame()
        {
            
            //reached last frame of current animation
            if (_currentIndex + 1 >= _animationQueue.Peek().Frames.Length)
            {
                //if there is another animation in queue, play it! (also remove current animation)
                if (_animationQueue.Count > 1)
                {
                    _animationQueue.Dequeue();
                    _currentIndex = 0;
                }
                else if (_animationQueue.Peek().Loop)
                {
                    _currentIndex = 0;
                }

            }
            else
            {
                _currentIndex++;
            }

            int frame = _animationQueue.Peek().Frames[_currentIndex];
            int indexX = frame % _rows;
            int indexY = frame / _rows;
            base.TextureRect = new IntRect(indexX * _tileWidth, indexY * _tileHeight, _tileWidth, _tileHeight);
        }
    }
}
