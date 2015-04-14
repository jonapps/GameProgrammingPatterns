using SFML.Graphics;
using SFML.Window;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Pong;
using JGerdesJWiemers.Game.Pong.Screens;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Inputs;

namespace JGerdesJWiemers.Game
{
    class Game
    {
        public static readonly string GAME_TITLE = "Pong";
        public static long MS_PER_UPDATE = 15;

        private RenderWindow _window;
        private Stopwatch _stopWatch;
        private ScreenManager _screenManager;
        private InputManager _inputManager;
     
        /// <summary>
        /// Starts the app
        /// </summary>
        public void Start()
        {
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 8;
            this._window = new RenderWindow(new VideoMode(1280, 720), GAME_TITLE, Styles.Default, settings);
            this._stopWatch = new Stopwatch();
            this._screenManager = new ScreenManager();
            this._screenManager.CurrentScreen = new GameScreen();
            this._inputManager = InputManager.Instance;
            this.Run();
        }


        private void _SendMsg()
        {
            Console.WriteLine("check: das aus");
        }


        /// <summary>
        /// 
        /// </summary
        private void _Update()
        {
            if (this._inputManager.IsKeyPressed(Keyboard.Key.C))
            {
                this._SendMsg();
            }

            _window.KeyPressed += this._inputManager.KeyPressed;
            _window.KeyReleased += this._inputManager.KeyReleased;
            _window.DispatchEvents();
            this._screenManager.Update();
        }
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="delta"></param>
        private void _Render(float delta)
        {
            _window.Clear();
            this._screenManager.Render(_window, delta);
            _window.Display();
        }

        /// <summary>
        /// Where the gameloop takes place
        /// </summary>
        private void Run()
        {
            this._stopWatch.Start();
            long elapsed = 0;
            long lag = 0;
            while (this._window.IsOpen())
            {
                elapsed = this._stopWatch.ElapsedMilliseconds;
                this._stopWatch.Restart();
                lag += elapsed;
                while (lag >= MS_PER_UPDATE)
                {
                    _Update();
                    lag -= MS_PER_UPDATE;
                }
                this._Render(lag / (float)MS_PER_UPDATE);
            }
        }

       

    }
}
