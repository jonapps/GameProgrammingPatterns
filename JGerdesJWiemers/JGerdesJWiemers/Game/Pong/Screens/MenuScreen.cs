using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.Pong.Screens.UI;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Pong.Screens
{
    class MenuScreen : Screen
    {

        private ButtonManager _buttonManager;
        private Sprite _background;

        public MenuScreen(Window w) : base(w)
        {
            _buttonManager = new ButtonManager();

            Button pvpButton = new Button(1280 / 2f, 720 / 6f, "Player VS Player");
            pvpButton.OnSelected += delegate
            {
                _screenManager.CurrentScreen = new GameScreen(w, GameScreen.GameType.PlayerVsPlayer);
            };

            Button pvnButton = new Button(1280 / 2f, 720 - 720/ 6f, "Player VS NPC");
            pvnButton.OnSelected += delegate
            {
                _screenManager.CurrentScreen = new GameScreen(w, GameScreen.GameType.PlayerVsNPC);
            };

            Button exitButton = new Button(1280 - 100, 720 - 50, "Exit");
            exitButton.OnSelected += delegate
            {
                w.Close();
            };

            _buttonManager.AddButton(pvpButton);
            _buttonManager.AddButton(pvnButton);
            _buttonManager.AddButton(exitButton);

            _background = new Sprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_BACKGROUND));

        }

        public override void Update()
        {
            _buttonManager.Update();
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            renderTarget.Draw(_background);
            _buttonManager.Render(renderTarget, extra);
        }

        public override void Exit()
        {
            
        }
    }
}
