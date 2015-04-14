using SFML.Graphics;
using SFML.Window;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace JGerdesJWiemers.Game
{
    class Game
    {
        public static readonly string GAME_TITLE = "Pong";
        private RenderWindow _window;
        private Stopwatch _stopWatch;
        private static long _MS_PER_UPDATE = 15;
     
        /// <summary>
        /// Starts the app
        /// </summary>
        public void Start()
        {
            this._window = new RenderWindow(new VideoMode(1280, 720), GAME_TITLE, Styles.Default);
            this._stopWatch = new Stopwatch();
            this.Run();
        }

        private double _GetCurrentTime()
        {
            return 0.0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delta"></param>
        private void _Update(long delta)
        {
            Console.WriteLine("update: " + delta);
            _window.Clear();
            _window.DispatchEvents();
            Thread.Sleep(13);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delta"></param>
        private void _Render()
        {
            Console.WriteLine("Render");
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
                    _Update(lag / _MS_PER_UPDATE);
                    lag -= _MS_PER_UPDATE;
                }
                this._Render();
            }
        }

       

    }
}
