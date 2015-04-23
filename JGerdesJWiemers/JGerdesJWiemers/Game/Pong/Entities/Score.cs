using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Graphics;

namespace JGerdesJWiemers.Game.Pong.Entities
{
    class Score : IRenderable
    {
        private int _score;
        private Font _font;
        private Text _text;

        public Score(Font font, Vector2f position, Color color)
            :base()
        {
            _font = font;
            _text = new Text();
            _text.Font = font;
            _score = 0;
            _text.DisplayedString = "0";
            _text.Position = position;
            _text.Color = color;

        }

        public int Value
        {
            get
            {
                return _score;
            }
        }

        public void Render(RenderTarget renderTarget, float extra)
        {
            renderTarget.Draw(_text);
        }

        public int addToScore(int change)
        {
            _score += change;
            _text.DisplayedString = _score.ToString();
            return _score;
        }
    }


}
