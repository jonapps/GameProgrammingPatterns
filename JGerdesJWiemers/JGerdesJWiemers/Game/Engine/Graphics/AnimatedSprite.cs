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

        private Queue<Animation> _animationQueue;

        private int _currentIndex;
        private long _startTime;

        public delegate void AnimationEventHandler(Animation animation);
        public event AnimationEventHandler OnAnimationEnded;

        public AnimatedSprite(Texture tex, int tileWidth, int tileHeight, Animation animation)
            : base(tex)
        {
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _rows = (int) tex.Size.X / _tileWidth;
            _columns = (int) tex.Size.Y / _tileHeight;

            _startTime = Game.ElapsedTime;

            _animationQueue = new Queue<Animation>();
            TextureRect = new IntRect(0, 0, _tileWidth, _tileHeight);          

            SetAnimation(animation);
            _RecalculateTextureRect();
        }

        public AnimatedSprite(Texture tex, int tileWidth, int tileHeight)
            : this(tex, tileWidth, tileHeight, new Animation()) { }

        public AnimatedSprite(TextureContainer cont)
            : this(cont.Texture, cont.Width, cont.Height) { }

        public void CenterOrigin()
        {
            Origin = new SFML.System.Vector2f(_tileWidth / 2f, _tileHeight / 2f);
        }

        public void Update()
        {
            Console.WriteLine(Game.ElapsedTime);
            if (Game.ElapsedTime - _startTime >= _animationQueue.Peek().Duration)
            {
                _NextFrame();
                _startTime = Game.ElapsedTime;
            }
        }

        public new void Draw(RenderTarget renderTarget, RenderStates renderStates)
        {
            base.Draw(renderTarget, renderStates);
        }


        public void SetAnimation(Animation animation)
        {
            _animationQueue.Clear();
            _animationQueue.Enqueue(animation);
            _currentIndex = 0;
            _RecalculateTextureRect();
        }

        public void EnqueueAnimation(Animation animation)
        {
            _animationQueue.Enqueue(animation);
        }

        public int GetFrameCount()
        {
            return _rows * _columns;
        }

        private void _NextFrame()
        {
            
            //reached last frame of current animation
            if (_currentIndex + 1 >= _animationQueue.Peek().Frames.Length)
            {
                if (!_animationQueue.Peek().Loop && OnAnimationEnded != null)
                {
                    OnAnimationEnded(_animationQueue.Peek());
                }
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

            _RecalculateTextureRect();
        }

        private void _RecalculateTextureRect()
        {
            int frame = _animationQueue.Peek().Frames[_currentIndex];
            int indexX = frame % _rows;
            int indexY = frame / _rows;
            base.TextureRect = new IntRect(indexX * _tileWidth, indexY * _tileHeight, _tileWidth, _tileHeight);
        }

    }
}
