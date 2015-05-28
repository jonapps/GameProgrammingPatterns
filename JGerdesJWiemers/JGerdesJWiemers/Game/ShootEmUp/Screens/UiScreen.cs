using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Graphics.Screens;
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

namespace JGerdesJWiemers.Game.ShootEmUp.Screens
{
    class UiScreen : Screen
    {
        private static readonly string TEXT_PRE_SCORE = "Score: ";

        private Sprite _bg;
        private Sprite _shuttle;
        private AnimatedSprite _earth;
        private View _view;
        private List<Text> _texts;
        private Text _score;

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
            _earth.Color = new Color(0, 255, 0, 200);

            _shuttle = new Sprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_UI_SHUTTLE).Texture);
            _shuttle.Origin = new Vector2f(_shuttle.GetLocalBounds().Width / 2f, _shuttle.GetLocalBounds().Height / 2f);
            _shuttle.Texture.Smooth = false;
            _shuttle.Scale *= 1.6f;
            _shuttle.Position = _bg.Position + new Vector2f(1040, -34);
            _shuttle.Color = new Color(0, 255, 0, 200);

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
            
            _texts.Add(_score);

        }

        public void UpdateScore(int score)
        {
            _score.DisplayedString = "" + score;
            FloatRect bounds = _score.GetLocalBounds();
            _score.Origin = new Vector2f(bounds.Width, 0);
        }

        public override void Update()
        {
            _earth.Update();
            
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
            throw new NotImplementedException();
        }

        public override bool DoRenderBelow()
        {
            return true;
        }

        public override bool DoUpdateBelow()
        {
            return true;
        }
    }
}
