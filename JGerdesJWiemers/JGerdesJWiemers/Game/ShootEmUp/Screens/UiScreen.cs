using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.Engine.Input;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.ShootEmUp.Logic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMath = System.Math;

namespace JGerdesJWiemers.Game.ShootEmUp.Screens
{
    class UiScreen : Screen
    {
        private static readonly string TEXT_PRE_SCORE = "Score: ";
        private static readonly string TEXT_PRE_ASTRONAUTS = "Astronauts: ";
        private static readonly string TEXT_PRE_WAVE = "Wave: ";

        private Sprite _bg;
        private Sprite _shuttle;
        private AnimatedSprite _earth;
        private View _view;
        private List<Text> _texts;
        private Text _score;
        private Text _wave;
        private Text _astronauts;

        public UiScreen(RenderWindow w)
            :base(w)
        {
            _bg = new Sprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_UI).Texture);
            _bg.Origin = new Vector2f(0, _bg.GetLocalBounds().Height);
            _bg.Position = new Vector2f(0, 720);

            TextureContainer earthTex = AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_UI_EARTH);
            _earth = new AnimatedSprite(earthTex.Texture, earthTex.Width, earthTex.Height);
            _earth.Origin = new Vector2f(earthTex.Width / 2f, earthTex.Height / 2f);
            _earth.Position = _bg.Position + new Vector2f(972, -34);
            _earth.SetAnimation(new Animation(0, 15, 500, true, false));
            _earth.Texture.Smooth = false;
            _earth.Scale *= 1.6f;
            _earth.Color = new Color(0, 255, 0, 160);

            _shuttle = new Sprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_UI_SHUTTLE).Texture);
            _shuttle.Origin = new Vector2f(_shuttle.GetLocalBounds().Width / 2f, _shuttle.GetLocalBounds().Height / 2f);
            _shuttle.Texture.Smooth = false;
            _shuttle.Scale *= 1.6f;
            _shuttle.Position = _bg.Position + new Vector2f(1040, -34);
            _shuttle.Color = new Color(0, 255, 0, 160);

            Vector2f size = new Vector2f(w.Size.X, w.Size.Y);
            _view = new View(size / 2f, size);

            _texts = new List<Text>();

            Text scoreText = new Text(TEXT_PRE_SCORE, AssetLoader.Instance.getFont(AssetLoader.FONT_DIGITAL));
            scoreText.Color = new Color(0, 255, 96);
            scoreText.CharacterSize = 14;
            scoreText.Position = _bg.Position + new Vector2f(118, -62);
            _texts.Add(scoreText);

            _score = new Text(scoreText);
            UpdateScore(GameManager.Instance.Score);
            _score.Position = _bg.Position + new Vector2f(320, -62);

            Text astroText = new Text(scoreText);
            astroText.Position = _bg.Position + new Vector2f(118, -44);
            astroText.DisplayedString = TEXT_PRE_ASTRONAUTS;
            _texts.Add(astroText);

            _astronauts = new Text(scoreText);
            UpdateAstronauts(GameManager.Instance.Astronauts);
            _astronauts.Position = _bg.Position + new Vector2f(320, -44);
            _texts.Add(_astronauts);

            Text waveText = new Text(scoreText);
            waveText.Position = _bg.Position + new Vector2f(118, -26);
            waveText.DisplayedString = TEXT_PRE_WAVE;
            _texts.Add(waveText);

            _wave = new Text(scoreText);
            UpdateWave(GameManager.Instance.CurrentWave);
            _wave.Position = _bg.Position + new Vector2f(320, -26);
            _texts.Add(_wave);
            
            _texts.Add(_score);



            //test
            int i = 100;
            _input.On("test1", delegate(InputEvent e, int c){
                i -= 1;
                Console.WriteLine(i);
                UpdateEarthHealth(i);
                return false;
            });

        }

        public void UpdateScore(int score)
        {
            _score.DisplayedString = "" + score;
            FloatRect bounds = _score.GetLocalBounds();
            _score.Origin = new Vector2f(bounds.Width, 0);
        }

        public void UpdateAstronauts(int astronauts)
        {
            _astronauts.DisplayedString = "" + astronauts;
            FloatRect bounds = _astronauts.GetLocalBounds();
            _astronauts.Origin = new Vector2f(bounds.Width, 0);
        }

        public void UpdateWave(int wave)
        {
            _wave.DisplayedString = "" + wave;
            FloatRect bounds = _wave.GetLocalBounds();
            _wave.Origin = new Vector2f(bounds.Width, 0);
        }

        public void UpdateEarthHealth(int health)
        {
            _earth.Color = _HealthToColor(health, 100);
        }

        public void UpdateShipHealth(int health)
        {
            _shuttle.Color = _HealthToColor(health, 100);
        }

        public override void Update()
        {
            _earth.Update();
            UpdateEarthHealth(GameManager.Instance.EarthHealth);
            
        }

        public override void PastUpdate()
        {
            
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            renderTarget.SetView(_view);
            renderTarget.Draw(_bg);
            renderTarget.Draw(_earth); 
            renderTarget.Draw(_shuttle);
            foreach (Text t in _texts)
            {
                renderTarget.Draw(t);
            }
        }

        public override void Exit()
        {

        }

        public override bool DoRenderBelow()
        {
            return true;
        }

        public override bool DoUpdateBelow()
        {
            return true;
        }


        private Color _HealthToColor(float health, int max)
        {
            //sqrt needed for gamma correction
            double green = (255 * SMath.Sqrt(health / max));
            health = max - health;
            double red = (255 * SMath.Sqrt(health / max));

            byte bRed = (byte)SMath.Min(255, SMath.Max(0, red));
            byte bGreen = (byte)SMath.Min(255, SMath.Max(0, green));

            return new Color(bRed, bGreen, 0, 200);
        }
    }
}
