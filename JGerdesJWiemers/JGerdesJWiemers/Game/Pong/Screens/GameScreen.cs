﻿using System;
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
    class GameScreen : Screen
    {
        private List<Entity> _entities;
        private Ball _ball;

        public GameScreen(Window w):base(w)
        {
            _entities = new List<Entity>();
            Paddle playerPaddle = new Paddle();
            playerPaddle.Controller = new Player(_window);
            _entities.Add(playerPaddle);
            _ball = new Ball(400, 400);
            _entities.Add(_ball);
           
        }

        public override void  Update()
        {
            foreach(Entity entity in _entities)
            {
                entity.Update();
                if (entity is Paddle)
                {
                    _ball.CollideWith((Paddle)entity);
                }
            }
        }
 
        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            foreach (Entity entity in _entities)
            {
                entity.Render(renderTarget, extra);
            }
        }
    }
}
