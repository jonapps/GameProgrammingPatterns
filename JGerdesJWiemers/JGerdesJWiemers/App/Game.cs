using SFML.Graphics;
using SFML.Window;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.App
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
        private void CheckKeys(object sender, EventArgs e)
        {
            KeyEventArgs ev = (KeyEventArgs)e;
            if (ev.Code == Keyboard.Key.Escape)
            {
                this.OnClose(sender, e);
            }
                
        }

        /// <summary>
        /// Where the gameloop takes place
        /// </summary>
        private void Run()
        {
            CircleShape cs = new CircleShape(100.0f);
            cs.SetPointCount(8);
            cs.Origin = new Vector2f(100, 100);
            Vector2f center = new Vector2f(window.Size.X,window.Size.Y) / 2;
            cs.Position = center;
            cs.FillColor = new Color(150, 255, 150);

            Font roboto = null;
            try
            {
                roboto = new Font(@"Assets\Fonts\Roboto-Light.ttf");
            }
            catch (SFML.LoadingFailedException lfe)
            {
                this.OnClose(window, null);
            }

            Text text = new Text("foobar", roboto);
            

            CircleShape point = new CircleShape(20f);
            point.Origin = new Vector2f(20, 20);
            point.FillColor = new Color(255, 150, 150);

            window.Closed += this.OnClose;
            window.KeyPressed += this.CheckKeys;
            window.SetActive();
            window.Capture();
            long start = this.getMillis();
            long now = start;
            while (this.window.IsOpen())
            {
                cs.Rotation -= 0.05f;
                now = this.getMillis() - start;
                point.Position = center + new Vector2f((float)Math.Cos(now/500f)*200f, (float)Math.Sin(now/500f)*200f);
                window.Clear();
                window.DispatchEvents();
                window.Draw(cs);
                window.Draw(point);
                window.Draw(text);
                window.Display();
            }
        }

        public long getMillis()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}
