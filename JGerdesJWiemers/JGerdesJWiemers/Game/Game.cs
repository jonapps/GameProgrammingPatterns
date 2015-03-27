using SFML.Graphics;
using SFML.Window;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game
{
    class Game
    {
        public static readonly string GAME_TITLE = "Pong";
        private RenderWindow _window;
     
        /// <summary>
        /// Starts the app
        /// </summary>
        public void Start()
        {
            this._window = new RenderWindow(new VideoMode(1280, 720), GAME_TITLE, Styles.Default);
            this.Run();
        }

        /// <summary>
        /// Where the gameloop takes place
        /// </summary>
        private void Run()
        {
            while (this._window.IsOpen())
            {
                _window.Clear();
                _window.DispatchEvents();
                _window.Display();
            }
        }

    }
}
