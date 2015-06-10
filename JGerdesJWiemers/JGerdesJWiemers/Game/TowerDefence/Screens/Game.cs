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
            w.MouseButtonPressed += _MouseButtonPressed;
            w.MouseMoved += w_MouseMoved;
        }

        void w_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            if (SFML.Window.Mouse.IsButtonPressed(Mouse.Button.Right))
            {
                Vector2f mapped = _map.ScreenToMap(new Vector2f(e.X, e.Y));
                Console.Write("\r("+e.X+",\t"+e.Y+"\t) on map: (\t"+mapped.X+",\t"+mapped.Y+")                     ");
            }
        }

        void _MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                Vector2i tile = _map.GetTileIndexAt(new Vector2f(e.X, e.Y));
                Console.WriteLine("Tile@("+e.X+","+e.Y+"):\tx:"+tile.X+"\ty:"+tile.Y);
            }
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
