using JGerdesJWiemers.Game.Engine.Audio;
using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.Engine.Utils;
using SFML.Graphics;
using SFML.System;
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

        private static readonly long DURATION = 2500;

        private Sprite _image;
        private float _opacity;
        private long _startTime;

        public SplashScreen(RenderWindow w)
            :base(w)
        {
            _image = new Sprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_SPLAH_AWSM).Texture);
            _image.Origin = new Vector2f(_image.GetLocalBounds().Width / 2f, _image.GetLocalBounds().Height / 2f);
            _image.Position = new Vector2f(w.Size.X / 2, w.Size.Y / 2);
            _startTime = JGerdesJWiemers.Game.Game.ElapsedTime;
            _opacity = 0;
            _clearColor = LevelSelector.AWSM_GREY;
            AudioManager.Instance.PlayMusic(AssetLoader.AUDIO_MUSIC_1);

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

        protected override void _Resize(Engine.EventSystem.Events.EngineEvent eventData)
        {
            base._Resize(eventData);
            Vector2f size = (Vector2f)eventData.Data;
            _image.Position = new Vector2f(size.X / 2, size.Y / 2);
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
