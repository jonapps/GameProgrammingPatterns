using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.Pong.Entities;
using JGerdesJWiemers.Game.Pong.Controller;
using SFML.Window;
using SFML.Graphics;
using System.Diagnostics;
using SFML.System;



namespace JGerdesJWiemers.Game.Pong.Screens
{
    class GameScreen : Screen
    {
        private List<Entity> _entities;
        private Ball _ball;
        private Score _score1;
        private Score _score2;

        private Sprite _background;

        public GameScreen(Window w):base(w)
        {
            _entities = new List<Entity>();
            _ball = new Ball(1, 1);
            _entities.Add(_ball);
            Paddle aiPaddle = new Paddle(new Rail(Rail.SIDE_RIGHT));
            aiPaddle.Controller = new Ai(_window, aiPaddle, _ball);
            Paddle playerPaddle = new Paddle(new Rail(Rail.SIDE_LEFT));
            playerPaddle.Controller = new Player(_window, playerPaddle);
            _entities.Add(playerPaddle);
            _entities.Add(aiPaddle);


            _score1 = new Score(AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_LIGHT),
                            new Vector2f(30, 20), new Color(146, 27, 37, 100));

            _score2 = new Score(AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_LIGHT),
                            new Vector2f(1280 - 50, 20), new Color(49, 27, 146, 100));

            _background = new Sprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_BACKGROUND));

           
           
        }

        public override void  Update()
        {
            foreach(Entity entity in _entities)
            {
                entity.Update();
                if (entity is Paddle)
                {
                    _ball.CollideWith((Paddle)entity, (RenderTarget)_window);
                }
            }

            if (_ball.Position.X < 0)
            {
                _score2.addToScore(1);
                _ball.reset();
            }
            else if (_ball.Position.X > 1280)
            {
                _score1.addToScore(1);
                _ball.reset();
            }
        }
 
        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            renderTarget.Draw(_background);
            foreach (Entity entity in _entities)
            {
                entity.Render(renderTarget, extra);
            }

            _score1.Render(renderTarget, extra);
            _score2.Render(renderTarget, extra);
        }
    }
}
