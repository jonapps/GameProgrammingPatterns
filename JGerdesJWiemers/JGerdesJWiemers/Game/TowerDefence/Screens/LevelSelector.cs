using JGerdesJWiemers.Game.Engine.Audio;
using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.Engine.Utils.Helper.LevelAssets;
using JGerdesJWiemers.Game.TowerDefence.UiElements;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMath = System.Math;

namespace JGerdesJWiemers.Game.TowerDefence.Screens
{
    class LevelSelector : Screen
    {

        private class LevelLabel : Label
        {
            public LevelAsset Asset;

            public LevelLabel(LevelAsset asset)
            : base(asset.Info.Name, AssetLoader.FONT_ROBOTO_THIN, 48, asset.Info.Preview){
                Asset = asset;
            }
        }

        public static Color WHITE = new Color(255, 255, 255, 255);
        public static Color AWSM_ORANGE = new Color(251, 140, 0);
        public static Color AWSM_GREY = new Color(66, 66, 66);

        private static float TITLE_PADDING_TOP = 40;

        //Dööö dööö döö, dö dö dö dö dö döö dö dö
        private List<LevelLabel> _levels;
        private int _currentIndex = 0;

        private Text _title;

        public LevelSelector(RenderWindow w)
        :base(w){
            _clearColor = AWSM_GREY;

            _levels = new List<LevelLabel>();

            AssetLoader.Instance.LoadEnemyTextures();
            List<LevelAsset> levelAssets = AssetLoader.Instance.GetLevels();
            Vector2f position = new Vector2f(_window.Size.X / 2f - 192, 220);
            int count = 0;
            foreach(LevelAsset la in levelAssets){
                LevelLabel ll = new LevelLabel(la);
                ll.Position = position + new Vector2f(0, count * 96);
                _levels.Add(ll);
                ++count;
            }

            _title = new Text("Select Level", AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_THIN));
            _title.CharacterSize = 48;
            _title.Origin = new Vector2f(_title.GetLocalBounds().Width / 2, 0);
            _title.Position = new Vector2f(w.Size.X / 2, TITLE_PADDING_TOP);
            updateView();


            _window.KeyPressed += _window_KeyPressed;
            AudioManager.Instance.PlayMusic(AssetLoader.AUDIO_MUSIC_1);

        }

        void _window_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.Return:
                    startLevel(_levels[_currentIndex].Asset);
                    AudioManager.Instance.PlaySound(AssetLoader.AUDIO_START_LEVEL);
                    break;
                case Keyboard.Key.Up:
                case Keyboard.Key.Right:
                case Keyboard.Key.W:
                case Keyboard.Key.D:
                     _currentIndex = SMath.Max(0, _currentIndex - 1);
                     AudioManager.Instance.PlaySound(AssetLoader.AUDIO_SELECT_LEVEL);
                    break;
                case Keyboard.Key.Down:
                case Keyboard.Key.Left:
                case Keyboard.Key.S:
                case Keyboard.Key.A:
                    _currentIndex = SMath.Min(_levels.Count - 1, _currentIndex + 1);
                    AudioManager.Instance.PlaySound(AssetLoader.AUDIO_SELECT_LEVEL);
                    break;
            }
            updateView();
        }

        public override void Exit()
        {

            _window.KeyPressed -= _window_KeyPressed;
        }

        public override void Update()
        {
            
        }

        public override void PastUpdate()
        {
           
        }

        public override void PreDraw(float extra)
        {
            
        }

        public override void Draw(SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states)
        {
            foreach (LevelLabel ll in _levels)
            {
                target.Draw(ll, states);
            }

            target.Draw(_title);
        }


        private void updateView()
        {
            foreach (LevelLabel ll in _levels)
            {
                ll.Color = WHITE;
            }

            _levels[_currentIndex].Color = AWSM_ORANGE;
        }

        private void startLevel(LevelAsset level)
        {
            _screenManager.Pop();
            _screenManager.Push(new Game(_window, level));
            _screenManager.Push(new CursorScreen(_window));
        }

        protected override void _Resize(Engine.EventSystem.Events.EngineEvent eventData)
        {
            base._Resize(eventData);
            Vector2f size = (Vector2f)eventData.Data;
            _title.Position = new Vector2f(size.X / 2, TITLE_PADDING_TOP);
        }
    }
}
