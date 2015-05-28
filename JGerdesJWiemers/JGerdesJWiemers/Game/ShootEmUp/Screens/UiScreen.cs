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
        private Text _earthHealth;
        private Text _shipHealth;
        private Text _bulletAmount;
        private Text _rocketAmount;
        private List<AnimatedSprite> _weapons;

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
            UpdateScore(0);
            _score.Position = _bg.Position + new Vector2f(320, -62);
            _texts.Add(_score);

            Text astroText = new Text(scoreText);
            astroText.Position = _bg.Position + new Vector2f(118, -44);
            astroText.DisplayedString = TEXT_PRE_ASTRONAUTS;
            _texts.Add(astroText);

            _astronauts = new Text(scoreText);
            UpdateAstronauts(0);
            _astronauts.Position = _bg.Position + new Vector2f(320, -44);
            _texts.Add(_astronauts);

            Text waveText = new Text(scoreText);
            waveText.Position = _bg.Position + new Vector2f(118, -26);
            waveText.DisplayedString = TEXT_PRE_WAVE;
            _texts.Add(waveText);

            _wave = new Text(scoreText);
            UpdateWave(0);
            _wave.Position = _bg.Position + new Vector2f(320, -26);
            _texts.Add(_wave);

            _earthHealth = new Text(scoreText);
            _earthHealth.CharacterSize = 20;
            _earthHealth.Position = _bg.Position + new Vector2f(1000, -29);
            _earthHealth.Color = new Color(240, 240, 255);
            _texts.Add(_earthHealth);

            _shipHealth = new Text(_earthHealth);
            _shipHealth.Position = _bg.Position + new Vector2f(1068, -29);
            _texts.Add(_shipHealth);

            _bulletAmount = new Text(scoreText);
            _bulletAmount.CharacterSize = 18;
            _bulletAmount.Position = _bg.Position + new Vector2f(60, -57);
            _texts.Add(_bulletAmount);

            Text bulletX = new Text(_bulletAmount);
            bulletX.DisplayedString = "x";
            bulletX.CharacterSize = 10;
            bulletX.Position = _bg.Position + new Vector2f(53, -50);
            _texts.Add(bulletX);

            _rocketAmount = new Text(_bulletAmount);
            _rocketAmount.Position = _bg.Position + new Vector2f(60, -30);
            _texts.Add(_rocketAmount);

            Text rocketX = new Text(bulletX);
            rocketX.Position = _bg.Position + new Vector2f(53, -23);
            _texts.Add(rocketX);

            _weapons = new List<AnimatedSprite>();

            _weapons.Add(new AnimatedSprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_UI_SINGLE)));
            _weapons.Add(new AnimatedSprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_UI_DOUBLE)));
            _weapons.Add(new AnimatedSprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_UI_MISSILE)));

            _weapons[0].Position = _bg.Position + new Vector2f(40, -55);
            _weapons[1].Position = _bg.Position + new Vector2f(40, -39);
            _weapons[2].Position = _bg.Position + new Vector2f(40, -18);

            foreach (AnimatedSprite s in _weapons)
            {
                s.CenterOrigin();
                s.Color = new Color(0, 255, 0, 200);
                s.Scale *= 0.5f;
            }

            UpdateBulletAmount(GameManager.Instance.GetRoundsLeft());
            UpdateRocketAmount(GameManager.Instance.GetRocketsLeft());
            SetCurrentWeapon(0);

            UpdateShipHealth(100);
            UpdateEarthHealth(100);

            //test
            int i = 100;
            _input.On("test1", delegate(InputEvent e, int c){
                i -= 1;
                Console.WriteLine(i);
                UpdateEarthHealth(i);
                return false;
            });


            GameManager.Instance.OnAstronautChange += delegate(int newval)
            {
                UpdateAstronauts(newval);
            };

            GameManager.Instance.OnEarthHealthChange += delegate(int newval)
            {
                UpdateEarthHealth(newval);
            };

            GameManager.Instance.OnScoreChange += delegate(int newval)
            {
                UpdateScore(newval);
            };

            GameManager.Instance.OnWaveChange += delegate(int newval)
            {
                UpdateWave(newval);
            };

            GameManager.Instance.OnPlayerHealthChange += delegate(int newval)
            {
                UpdateShipHealth(newval);
            };

            GameManager.Instance.OnCurrentWeaponChange += delegate(int index)
            {
                SetCurrentWeapon(index);
            };

            GameManager.Instance.OnRoundsChange += delegate(int amount)
            {
                UpdateBulletAmount(amount);
            };

            GameManager.Instance.OnRocketsChange += delegate(int amount)
            {
                UpdateRocketAmount(amount);
            };
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
            _earthHealth.DisplayedString = (100 - health) + "%";
            FloatRect bounds = _earthHealth.GetLocalBounds();
            _earthHealth.Origin = new Vector2f(bounds.Width, 0);
        }

        public void UpdateShipHealth(int health)
        {
            _shuttle.Color = _HealthToColor(health, 100);
            _shipHealth.DisplayedString = (100 - health) + "%";
            FloatRect bounds = _shipHealth.GetLocalBounds();
            _shipHealth.Origin = new Vector2f(bounds.Width, 0);
        }

        public void UpdateBulletAmount(int amount)
        {
            _bulletAmount.DisplayedString = "" + amount;
        }

        public void UpdateRocketAmount(int amount)
        {
            _rocketAmount.DisplayedString = "" + amount;
        }

        public void SetCurrentWeapon(int index)
        {
            for (int i = 0, c = _weapons.Count; i < c; ++i)
            {
                AnimatedSprite current = _weapons[i];
                if (i == index){
                   current.SetAnimation(new Animation(0,current.GetFrameCount() - 1, 200, true, false));
                   current.Color = new Color(255, 255, 255, 200);
                }
                else
                {
                    current.SetAnimation(new Animation());
                    current.Color = new Color(0, 255, 0, 200);
                }
                    
            }
        }

        public override void Update()
        {
            _earth.Update();
            foreach (AnimatedSprite s in _weapons)
            {
                s.Update();
            }
            
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
            foreach (AnimatedSprite s in _weapons)
            {
                renderTarget.Draw(s);
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

            return new Color(bRed, bGreen, 0, 100);
        }
    }
}
