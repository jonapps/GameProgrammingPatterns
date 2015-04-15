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


namespace JGerdesJWiemers.Game
{
    class Game
    {
        public static readonly string GAME_TITLE = "Pong";
        public static long MS_PER_UPDATE = 15;
        public static float PADDLE_GAME_SPEED = 50;

        private RenderWindow _window;
        private Stopwatch _stopWatch;
        private ScreenManager _screenManager;
        private Font _roboto = null;
        private long _millsSinceSec;
        private int _fps;
        private Text _fpsText;
        

     
        /// <summary>
        /// Starts the app
        /// </summary>
        public void Start()
        {
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 8;
            this._window = new RenderWindow(new VideoMode(1280, 720), GAME_TITLE, Styles.Default, settings);
            _window.KeyPressed += this._CloseGame;
            this._stopWatch = new Stopwatch();
            this._screenManager = new ScreenManager(_window);
            this._screenManager.CurrentScreen = new GameScreen(_window);
            _window.SetActive();
            _window.Closed += this._OnClose;
            try
            {
                _roboto = new Font(@"Assets\Fonts\Roboto-Light.ttf");
            }
            catch (SFML.LoadingFailedException lfe)
            {
                /// todo
            }
            _fpsText = new Text("fps: " + _fps, _roboto);
            this.Run();
        }

        /// <summary>
        /// Close the window when OnClose event is received
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }


        private void _CloseGame(Object window, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                ((Window)window).Close();
            }
        }

        


        /// <summary>
        /// 
        /// </summary
        private void _Update()
        {
            System.Console.WriteLine("Game.Update");
            _window.DispatchEvents();
            this._screenManager.Update();
        }

        private void _DrawFPS()
        {

            _window.Draw(_fpsText);
        }
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="delta"></param>
        private void _Render(float delta)
        {
            _window.Clear();
            _DrawFPS();
            _fps++;
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
                _millsSinceSec += elapsed;
                if (_millsSinceSec > 1000f)
                {
                    _fpsText = new Text("fps: " + _fps, _roboto);
                    _millsSinceSec = 0;
                    _fps = 0;
                }
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
