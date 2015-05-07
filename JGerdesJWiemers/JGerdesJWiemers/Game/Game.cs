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
using SFML.System;
using JGerdesJWiemers.Game.Engine.Input;
using FarseerPhysics;
using GameScreen = JGerdesJWiemers.Game.ShootEmUp.Screens;


namespace JGerdesJWiemers.Game
{
    class Game
    {
        public static readonly string VERSION = "v0.0.2";
        public static readonly string GAME_TITLE = "ShootEmUp";
        public static int ElapsedTime = 0;

        readonly Time TargetElapsedTime = Time.FromMicroseconds(16666);
        readonly Time MaxElapsedTime = Time.FromMicroseconds(10000);

        Time _accumulatedTime;
        Clock _clock;


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
            this._screenManager.CurrentScreen = new GameScreen.Game(_window);
            _window.SetActive();
            _window.Closed += this._OnClose;
            _window.SetVerticalSyncEnabled(true);
 

            InputManager.Init(_window);

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
            this._screenManager.Render(_window, delta);
            _window.Display();

        }

        /// <summary>
        /// Where the gameloop takes place
        /// </summary>
        private void Run()
        {
            _clock = new Clock();
            _accumulatedTime = Time.FromMicroseconds(0);
            while (_window.IsOpen)
            {
                Time elapsedTime = _clock.Restart();

                if (elapsedTime > MaxElapsedTime)
                {
                    elapsedTime = MaxElapsedTime;
                }
                _accumulatedTime += elapsedTime;
                bool updated = !false;
                while (_accumulatedTime >= TargetElapsedTime)
                {
                    _Update();
                    _accumulatedTime -= TargetElapsedTime;
                    //updated = true;
                }

                if (updated)
                {
                    float extra = _accumulatedTime.AsMilliseconds() / (float)TargetElapsedTime.AsMilliseconds();
                    _Render(0);
                    Game.ElapsedTime = _accumulatedTime.AsMilliseconds();
                }
            }
        }
    }
}
