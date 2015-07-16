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

        private static Color WHITE = new Color(255, 255, 255, 255);
        private static Color AWSM_ORANGE = new Color(253, 162, 71);

        //Dööö dööö döö, dö dö dö dö dö döö dö dö
        List<LevelLabel> levels;
        private int _currentIndex=0;

        public LevelSelector(RenderWindow w)
        :base(w){
            _clearColor = new Color(84, 84, 84);

            levels = new List<LevelLabel>();

            AssetLoader.Instance.LoadEnemyTextures();
            List<LevelAsset> levelAssets = AssetLoader.Instance.ReadLevels();
            Vector2f position = new Vector2f(_window.Size.X / 2f - 192, 100);
            int count = 0;
            foreach(LevelAsset la in levelAssets){
                LevelLabel ll = new LevelLabel(la);
                ll.Position = position + new Vector2f(0, count * 96);
                levels.Add(ll);
                ++count;
            }

            updateView();


            _window.KeyPressed += _window_KeyPressed;

        }

        void _window_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.Return:
                    startLevel(levels[_currentIndex].Asset);
                    break;
                case Keyboard.Key.Up:
                case Keyboard.Key.Right:
                case Keyboard.Key.W:
                case Keyboard.Key.D:
                     _currentIndex = SMath.Max(0, _currentIndex - 1);
                    break;
                case Keyboard.Key.Down:
                case Keyboard.Key.Left:
                case Keyboard.Key.S:
                case Keyboard.Key.A:
                    _currentIndex = SMath.Min(levels.Count - 1, _currentIndex + 1);
                    break;
            }
            updateView();
        }

        public override void Exit()
        {
            
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
            foreach (LevelLabel ll in levels)
            {
                target.Draw(ll, states);
            }
        }


        private void updateView()
        {
            foreach (LevelLabel ll in levels)
            {
                ll.Color = WHITE;
            }

            levels[_currentIndex].Color = AWSM_ORANGE;
        }

        private void startLevel(LevelAsset level)
        {
            _screenManager.Pop();
            _screenManager.Push(new Game(_window, level));
            _screenManager.Push(new CursorScreen(_window));
        }
    }
}
