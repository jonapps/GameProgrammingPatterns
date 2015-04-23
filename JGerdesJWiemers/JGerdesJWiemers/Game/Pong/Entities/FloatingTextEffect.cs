using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Utils;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Pong.Entities
{
    class FloatingTextEffect : IRenderable
    {
        private Text _text;
        private byte _alpha;

        public FloatingTextEffect(float x, float y, Color c, String text)
        {
            _text = new Text(text, AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_REGULAR));
            _text.Position = new Vector2f(x, y);
            _alpha = 192;
            _text.Color = c;
            _text.CharacterSize = 192;
            _text.Origin = new Vector2f(_text.GetGlobalBounds().Width / 2f, _text.GetGlobalBounds().Height / 2f);
        }


        public void Render(RenderTarget renderTarget, float extra)
        {
            _text.Position -= new Vector2f(0, 5f);
            if (_alpha >= 2)
            {
                _alpha -= 2;
            }
            _text.Color = new Color(_text.Color.R, _text.Color.G, _text.Color.B, _alpha);
            renderTarget.Draw(_text);
        }
    }
}
