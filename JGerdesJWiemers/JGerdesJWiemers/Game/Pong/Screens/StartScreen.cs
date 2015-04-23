using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Utils;

namespace JGerdesJWiemers.Game.Pong.Screens
{
    class StartScreen: Screen
    {

        private Text _startText;
        private Text _versionText;
        private Sprite _background;
        private float _alpha = 0;

        public StartScreen(Window w)
            : base(w)
        {
            _window.KeyPressed+= this._ProcessInput;
            _window.JoystickButtonPressed += this._ProcessInput;

            _startText = new Text("Press any key to start", AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_LIGHT));
            _startText.CharacterSize = 64;
            FloatRect bounds = _startText.GetGlobalBounds();
            _startText.Origin = new Vector2f(bounds.Width/2, bounds.Height/2);
            _startText.Position = new Vector2f(1280/2f, 560);
            _startText.Color = new Color(0, 0, 0);
 

            _versionText = new Text(Game.VERSION, AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_LIGHT));
            _versionText.Origin = new Vector2f(_versionText.GetGlobalBounds().Width, 0);
            _versionText.Position = new Vector2f(1280-30, 20);
            _versionText.Color = new Color(0, 0, 0);

            _background = new Sprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_TITLE_BACKGROUND));
        
        }

        private void _ProcessInput(Object sender,EventArgs e){
            _screenManager.CurrentScreen = new GameScreen(_window, GameScreen.GameType.NPCVsNPC);
        }

        public override void Update()
        {
            _alpha += 0.1f;
            if (_alpha > 2 * System.Math.PI)
            {
                _alpha -= 2 * (float)System.Math.PI;
            }
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            renderTarget.Draw(_background);
            double alpha = (System.Math.Sin(_alpha) + 1) * 128;
            _startText.Color = new Color(0, 0, 0, (byte)alpha);
            renderTarget.Draw(_startText);
            renderTarget.Draw(_versionText);
        }

        public override void Exit()
        {
            _window.KeyPressed -= this._ProcessInput;
            _window.JoystickButtonPressed -= this._ProcessInput;
        }
    }
}
