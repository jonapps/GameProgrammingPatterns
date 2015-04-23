using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Input;
using JGerdesJWiemers.Game.Engine.Utils;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Pong.Screens
{
    class GameOverScreen : Screen
    {

        private Sprite _medal;
        private Sprite _background;
        private Text _leftText;
        private Text _rightText;
        private Text _title;

        public GameOverScreen(Window w, int scoreLeft, int scoreRight)
            : base(w)
        {


            _leftText = new Text(""+scoreLeft, AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_LIGHT));
            _rightText = new Text("" + scoreRight, AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_LIGHT));
            _title = new Text(" wins!", AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_THIN));


            _leftText.CharacterSize = _rightText.CharacterSize = 64;
            _leftText.Origin = new Vector2f(_leftText.GetGlobalBounds().Width / 2f, _leftText.GetGlobalBounds().Height / 2f);
            _rightText.Origin = new Vector2f(_rightText.GetGlobalBounds().Width / 2f, _rightText.GetGlobalBounds().Height / 2f);
            _leftText.Position = new Vector2f(1280 / 4f, 720 - 720 / 6f);
            _rightText.Position = new Vector2f(1280 - 1280 / 4f, 720 - 720 / 6f);
            _leftText.Color = new Color(146, 27, 37, 100);
            _rightText.Color = new Color(49, 27, 146, 100);

            
            

            if(scoreLeft > scoreRight){
                _medal = new Sprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_MEDAL_RED));
                _medal.Position = new Vector2f(1280 / 4f, 720 / 2f);
                _title.DisplayedString = "Red " + _title.DisplayedString;
            }
            else
            {
                _medal = new Sprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_MEDAL_BLUE));
                _medal.Position = new Vector2f(1280 - 1280 / 4f, 720 / 2f);
                _title.DisplayedString = "Blue " + _title.DisplayedString;
            }

            _title.CharacterSize = 100;
            _title.Color = new Color(0, 0, 0, 250);
            _title.Origin = new Vector2f(_title.GetGlobalBounds().Width / 2f, _title.GetGlobalBounds().Height / 2f);
            _title.Position = new Vector2f(1280 / 2f, 720 / 6f);

            _medal.Origin = new Vector2f(_medal.GetGlobalBounds().Width / 2f, _medal.GetGlobalBounds().Height / 2f);

            _background = new Sprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_BACKGROUND));

            _window.KeyPressed += this._ProcessInput;
            _window.JoystickButtonPressed += this._ProcessInput;
            
        }

        private void _ProcessInput(Object sender, EventArgs e)
        {
            _screenManager.CurrentScreen = new MenuScreen(_window);
        }

        public override void Update()
        {
            
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            renderTarget.Draw(_background);
            renderTarget.Draw(_medal);
            renderTarget.Draw(_leftText);
            renderTarget.Draw(_rightText);
            renderTarget.Draw(_title);
        }

        public override void Exit()
        {
            _window.KeyPressed -= this._ProcessInput;
            _window.JoystickButtonPressed -= this._ProcessInput;
        }
    }
}
