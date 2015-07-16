using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.Engine.Utils;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMath = System.Math;

namespace JGerdesJWiemers.Game.TowerDefence.Screens
{
    class SplashScreen: Screen
    {

        private static readonly long DURATION = 30;
        private Sprite _image;
        private float _opacity;
        private long _startTime;

        public SplashScreen(RenderWindow w)
            :base(w)
        {
            _image = new Sprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_SPLAH_AWSM).Texture);
            _startTime = JGerdesJWiemers.Game.Game.ElapsedTime;
            _opacity = 0;

        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {

            _opacity += 0.01f;
            _opacity = SMath.Min(_opacity, 1);


            if (JGerdesJWiemers.Game.Game.ElapsedTime - _startTime > DURATION)
            {
                _screenManager.Switch(new LevelSelector(_window));
            }
        }

        public override void PastUpdate()
        {
            
        }

        public override void PreDraw(float extra)
        {
            
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            _image.Color = new Color(255, 255, 255, (byte)(_opacity * 255));
            target.Draw(_image);
        }
    }
}
