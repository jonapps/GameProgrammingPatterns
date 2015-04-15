using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Pong.Entities;
using JGerdesJWiemers.Game.Pong.Controller;
using SFML.Window;
using SFML.Graphics;
using System.Diagnostics;



namespace JGerdesJWiemers.Game.Pong.Screens
{
    class GameScreen : IScreen
    {
        private List<Entity> _entities;
        private Window _window;


        public GameScreen(Window w)
        {
            _window = w;
            _entities = new List<Entity>();
            Paddle playerPaddle = new Paddle();
            playerPaddle.Controller = new Player(_window);
            _entities.Add(playerPaddle);
            _entities.Add(new Ball(400, 400));
            
            
        }

        public void  Update()
        {
            foreach(Entity entity in _entities)
            {
                entity.Update();
            }
        }
 
        public void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            foreach (Entity entity in _entities)
            {
                entity.Render(renderTarget, extra);
            }
        }
    }
}
