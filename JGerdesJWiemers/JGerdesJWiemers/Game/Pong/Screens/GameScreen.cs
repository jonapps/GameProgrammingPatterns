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


        public static String SOUND_PADDLE_BALL_COLLISION = "SOUND_PADDLE_BALL_COLLISION";
        public static String SOUND_BACKGROUND = "SOUND_BACKGROUND";


        public enum GameType
        {
            PlayerVsPlayer,
            PlayerVsNPC,
            NPCVsNPC
        }

        private List<Entity> _entities;
        private Ball _ball;
        private Score _score1;
        private Score _score2;
        private Sprite _background;
        private CollisionSolver _colSolve;

        public GameScreen(Window w, GameType type):base(w)
        {
            AudioManager.Instance.AddSound(GameScreen.SOUND_PADDLE_BALL_COLLISION, "Assets/Audio/Bow_Fire_Arrow-Stephan_Schutze-2133929391.wav");
            AudioManager.Instance.AddSound(GameScreen.SOUND_BACKGROUND, "Assets/Audio/background.wav");
            AudioManager.Instance.Play(SOUND_BACKGROUND, 50, true);
            _colSolve = new CollisionSolver((RenderWindow)w);
            _entities = new List<Entity>();
            _ball = new Ball(1, 1, 10, 2f);
            _entities.Add(_ball);

            _SetUpGame(type);


            _score1 = new Score(AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_LIGHT),
                            new Vector2f(30, 20), new Color(146, 27, 37, 100));

            _score2 = new Score(AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_LIGHT),
                            new Vector2f(1280 - 50, 20), new Color(49, 27, 146, 100));

            _background = new Sprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_BACKGROUND));
        }

        private void _SetUpGame(GameType type)
        {
            Paddle rightPaddle = new Paddle(new Rail(Rail.SIDE_RIGHT), new Color(49, 27, 146, 80));            
            Paddle leftPaddle = new Paddle(new Rail(Rail.SIDE_LEFT), new Color(146, 27, 37, 80));
            
            switch (type)
            {
                case GameType.PlayerVsPlayer:
                    leftPaddle.Controller = new Player(leftPaddle, 0);
                    rightPaddle.Controller = new Player(rightPaddle, 1);
                    break;
                case GameType.PlayerVsNPC:
                    leftPaddle.Controller = new Player(leftPaddle, 0);
                    rightPaddle.Controller = new Ai(rightPaddle, _ball);
                    break;
                case GameType.NPCVsNPC:
                    leftPaddle.Controller = new Ai(leftPaddle, _ball);
                    rightPaddle.Controller = new Ai(rightPaddle, _ball);
                    break;
            }

            _entities.Add(leftPaddle);
            _entities.Add(rightPaddle);
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
                        AudioManager.Instance.Play(GameScreen.SOUND_PADDLE_BALL_COLLISION);
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
