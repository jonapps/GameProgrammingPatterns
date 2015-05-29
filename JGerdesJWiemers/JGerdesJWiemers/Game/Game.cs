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
using JGerdesJWiemers.Game.ShootEmUp.Screens;


namespace JGerdesJWiemers.Game
{
    class Game
    {
        public static readonly string VERSION = "v1.0";
        public static readonly string GAME_TITLE = "ShootEmUp";
        public static int ElapsedFrameTime = 0;
        public static long ElapsedTime = 0;
        public static bool DEBUG = !true;

        readonly Time TargetElapsedTime = Time.FromMicroseconds(16666);
        readonly Time MaxElapsedTime = Time.FromMicroseconds(10000);

        Time _accumulatedTime;
        Clock _clock;


        private RenderWindow _window;
        private Stopwatch _stopWatch;
        private ScreenManager _screenManager;
        

     
        /// <summary>
        /// Starts the app
        /// </summary>
        public void Start()
        {
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = 16;
            Settings.MaxPolygonVertices = 32;
            this._window = new RenderWindow(new VideoMode(1280, 720), GAME_TITLE, Styles.Default, settings);
            _window.KeyPressed += this._CloseGame;
            this._stopWatch = new Stopwatch();
            InputManager.Instance.Init(_window);
            this._screenManager = new ScreenManager(_window);
            this._screenManager.Push(new TitleScreen(_window));
            //this._screenManager.Push(new Editor.EditorScreen(_window));
            _window.SetActive();
            _window.Closed += this._OnClose;
            _window.SetVerticalSyncEnabled(true);
 

           
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
                Game.ElapsedTime += elapsedTime.AsMilliseconds();

                if (elapsedTime > MaxElapsedTime)
                {
                    elapsedTime = MaxElapsedTime;
                }
                _accumulatedTime += elapsedTime;
                bool updated = !false;
                while (_accumulatedTime >= TargetElapsedTime)
                {
                    _Update();
                    _PastUpdate();
                    _accumulatedTime -= TargetElapsedTime;
                    //updated = true;
                }

                if (updated)
                {
                    _Render(_accumulatedTime.AsSeconds());
                    Game.ElapsedFrameTime = _accumulatedTime.AsMilliseconds();
                }
            }
        }

        private void _PastUpdate()
        {
            this._screenManager.PastUpdate();
        }


    }
}
