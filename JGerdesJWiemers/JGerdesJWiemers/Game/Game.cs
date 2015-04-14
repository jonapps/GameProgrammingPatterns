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
using JGerdesJWiemers.Game.Pong;
using JGerdesJWiemers.Game.Pong.Screens;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Inputs;

namespace JGerdesJWiemers.Game
{
    class Game
    {
        public static readonly string GAME_TITLE = "Pong";
        private RenderWindow _window;
        private Stopwatch _stopWatch;
        public static long _MS_PER_UPDATE = 15;
        private ScreenManager _screenManager;
        private InputManager _inputManager;
     
        /// <summary>
        /// Starts the app
        /// </summary>
        public void Start()
        {
            this._window = new RenderWindow(new VideoMode(1280, 720), GAME_TITLE, Styles.Default);
            this._stopWatch = new Stopwatch();
            this._screenManager = new ScreenManager();
            this._screenManager.CurrentScreen = new GameScreen();
            this._inputManager = InputManager.Instance;
            this.Run();
        }


        /// <summary>
        /// 
        /// </summary
        private void _Update()
        {
            _window.KeyPressed += this.CheckKeys;
            _window.Clear();
            _window.DispatchEvents();
            this._screenManager.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delta"></param>
        private void _Render(long delta)
        {
            Console.WriteLine("Render: "+ delta);
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
                while (lag >= _MS_PER_UPDATE)
                {
                    _Update();
                    lag -= _MS_PER_UPDATE;
                }
                this._Render(lag / _MS_PER_UPDATE);
            }
        }

       

    }
}
