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
        /// <summary>
        /// Starts the app
        /// </summary>
        public void start()
        {
            this.window = new RenderWindow(new VideoMode(800, 600),"JGerdesJwiemers");
            this.run();

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
        /// Where the gameloop takes place
        /// </summary>
        private void run()
        {
            CircleShape cs = new CircleShape(100.0f);
            cs.FillColor = Color.Green;
            window.Closed += this.OnClose;
            window.SetActive();
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
