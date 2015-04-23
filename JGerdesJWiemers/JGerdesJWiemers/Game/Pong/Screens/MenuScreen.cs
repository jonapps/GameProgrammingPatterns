using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Pong.Screens.UI;
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

        public MenuScreen(Window w) : base(w)
        {
            _buttonManager = new ButtonManager();

            Button pvpButton = new Button(1280 / 2f, 200, "Player VS Player");
            pvpButton.OnSelected += delegate
            {
                _screenManager.CurrentScreen = new GameScreen(w, GameScreen.GameType.PlayerVsPlayer);
            };

            Button pvnButton = new Button(1280 / 2f, 400, "Player VS NPC");
            pvpButton.OnSelected += delegate
            {
                _screenManager.CurrentScreen = new GameScreen(w, GameScreen.GameType.PlayerVsNPC);
            };

            Button exitButton = new Button(1280 - 200, 720 - 200, "Exit");
            exitButton.OnSelected += delegate
            {
                w.Close();
            };

            _buttonManager.AddButton(pvpButton);
            _buttonManager.AddButton(pvnButton);
            _buttonManager.AddButton(exitButton);

        }

        public override void Update()
        {
            _buttonManager.Update();
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            _buttonManager.Render(renderTarget, extra);
        }

        public override void Exit()
        {
            
        }
    }
}
