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
using GameScreen = JGerdesJWiemers.Game.TowerDefence.Screens;
using JGerdesJWiemers.Game.TowerDefence.Screens;
using JGerdesJWiemers.Game.Engine.EventSystem;
using JGerdesJWiemers.Game.Engine.EventSystem.Events;
using System.IO;


namespace JGerdesJWiemers.Game
{
    class Game
    {
        public static readonly string VERSION = "v0.91";
        public static readonly string GAME_TITLE = "AWSM";
        public static long ElapsedTime = 0;
        public static bool DEBUG = !true;

        public static readonly string EVENT_RESIZE = "window.resize";


        public static readonly Time TargetElapsedTime = Time.FromMilliseconds(16);
        readonly Time MaxElapsedTime = Time.FromMilliseconds(25);

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
            ConvertUnits.SetDisplayUnitToSimUnitRatio(64f);
            this._window = new RenderWindow(new VideoMode(1280, 720), GAME_TITLE + " " + VERSION, Styles.Default, settings);
            _window.KeyPressed += this._CloseGame;
            this._stopWatch = new Stopwatch();
            InputManager.Instance.Init(_window);
            this._screenManager = new ScreenManager(_window);
            this._screenManager.Push(new SplashScreen(_window));
            //this._screenManager.Push(new Editor.EditorScreen(_window));
            _window.SetActive();
            _window.Closed += this._OnClose;
            _window.Resized += _window_Resized;

            _window.SetVerticalSyncEnabled(true);
            _window.SetMouseCursorVisible(false);
            //doesn't seem to work :(
            //setIcon(@"Assets\Graphics\icon.ico");
            this.Run();
        }


        void _window_Resized(object sender, SizeEventArgs e)
        {
            EventStream.Instance.Emit(EVENT_RESIZE, new EngineEvent(new Vector2f(e.Width, e.Height)));
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
            this._screenManager.PreDraw(delta);
            _window.Draw(this._screenManager);
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
                while (_accumulatedTime >= TargetElapsedTime)
                {
                    _Update();
                    _PastUpdate();
                    _accumulatedTime -= TargetElapsedTime;
                }
                _Render(_accumulatedTime.AsSeconds());
            }
        }

        private void _PastUpdate()
        {
            this._screenManager.PastUpdate();
        }

        private void setIcon(String file)
        {
            System.Drawing.Icon icon = new System.Drawing.Icon(file);
            MemoryStream ms = new MemoryStream();
            icon.Save(ms);
            byte[] data = ms.ToArray();
            _window.SetIcon((uint)icon.Width, (uint)icon.Height, data);
            icon.Dispose();
            ms.Close();
        }


    }
}
