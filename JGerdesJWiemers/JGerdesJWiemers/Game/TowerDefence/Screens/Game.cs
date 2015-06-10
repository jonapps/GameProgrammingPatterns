using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.TowerDefence.World;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.Screens
{
    class Game : Screen
    {
        private Map _map;
        public Game(RenderWindow w)
            :base(w)
        {
            _map = new Map(24, 16, 48);
            w.MouseMoved += w_MouseMoved;
        }

        void w_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            Shape s = _map.GetTileAt(new Vector2f(e.X, e.Y));
            if (SFML.Window.Mouse.IsButtonPressed(Mouse.Button.Right))
            {
                if (s != null)
                    s.FillColor = new Color(255, 128, 128, 120);
            }
            if (SFML.Window.Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                if (s != null)
                    s.FillColor = new Color(128, 128, 255, 120);
            }
            Vector2i tile = _map.GetTileIndexAt(new Vector2f(e.X, e.Y));
            Console.Write("\rTile@(" + e.X + "," + e.Y + "):\tx:" + tile.X + "\ty:" + tile.Y);
          
        }


        public override void Update()
        {
            
        }

        public override void PastUpdate()
        {
            
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            _map.Render(renderTarget, extra);
        }

        public override void Exit()
        {
            
        }
    }
}
