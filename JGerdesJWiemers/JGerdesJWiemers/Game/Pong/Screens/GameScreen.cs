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
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Audio;
using SFML.Audio;


namespace JGerdesJWiemers.Game.Pong.Screens
{
    class GameScreen : Screen
    {

        public static String PADDLE_BALL_COLLISION = "PADDLE_BALL_COLLISION";

        private List<Entity> _entities;
        private Ball _ball;
        private Score _score1;
        private Score _score2;
        private Sprite _background;
        private CollisionSolver _colSolve;
        private AudioManager _audioM;

        public GameScreen(Window w):base(w)
        {
            AudioManager.Instance.AddSound(GameScreen.PADDLE_BALL_COLLISION, "Assets/Audio/Bow_Fire_Arrow-Stephan_Schutze-2133929391.wav");

            _colSolve = new CollisionSolver((RenderWindow)w);
            _entities = new List<Entity>();
            _ball = new Ball(1, 1, 10, 2f);
            _entities.Add(_ball);
            Paddle aiPaddle = new Paddle(new Rail(Rail.SIDE_RIGHT), new Color(49, 27, 146, 80));
            aiPaddle.Controller = new Ai(aiPaddle, _ball);
            Paddle playerPaddle = new Paddle(new Rail(Rail.SIDE_LEFT), new Color(146, 27, 37, 80));
            playerPaddle.Controller = new Player(playerPaddle, 0);
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
                if (entity is RectangleEntity)
                {
                    if (_colSolve.solve((RectangleEntity)entity, _ball))
                    {
                        AudioManager.Instance.Play(GameScreen.PADDLE_BALL_COLLISION);
                        ((RectangleEntity)entity).onCollision();
                    }
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

        public override void Exit()
        {
            
        }
    }
}
