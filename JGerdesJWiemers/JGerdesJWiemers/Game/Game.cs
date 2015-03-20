using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.GameIternal
{
    class Game
    {

        private RenderWindow window;

        private Event runEvent;


        /// <summary>
        /// Starts the app
        /// </summary>
        public void Start()
        {
            this.window = new RenderWindow(new VideoMode(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height), "JGerdesJwiemers", Styles.Fullscreen);
            this.Run();
        }

        /// <summary>
        /// Close the window when OnClose event is received
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        /// <summary>
        /// Closes the window on esc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckKeys(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                RenderWindow window = (RenderWindow)sender;
                window.Close();
            }
                
        }

        /// <summary>
        /// Where the gameloop takes place
        /// </summary>
        private void Run()
        {
            CircleShape cs = new CircleShape(100.0f);
            cs.FillColor = Color.Green;
            window.Closed += this.OnClose;
            window.KeyPressed += this.CheckKeys;
            window.SetActive();
            window.Capture();
            while (this.window.IsOpen())
            {

                window.Clear();
                window.DispatchEvents();
                window.Draw(cs);
                window.Display();
            }
        }
    }
}
