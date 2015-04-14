using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Pong.Entities;



namespace JGerdesJWiemers.Game.Pong.Screens
{
    class GameScreen : IScreen
    {
        private List<Entity> _entities;

        public GameScreen()
        {
            _entities = new List<Entity>();
            _entities.Add(new Paddle());
        }

        public void Update()
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
