using JGerdesJWiemers.Game.Engine.Utils;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.UiElements
{
    class Label:Drawable
    {

        private static readonly float PADDING = 10;

        private Text _text;
        private Sprite _icon;
        private Color _color;
        private Vector2f _position;
        private String _displayedString;

        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                _text.Color = value;
                if(_icon != null)
                    _icon.Color = value;
            }   
        }

        public Vector2f Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                _text.Position = value;
                if (_icon != null)
                {
                    _icon.Position = value;
                    _text.Position += new Vector2f(_icon.TextureRect.Width +  PADDING, 0);
                }

            }
        }

        public String DisplayedString
        {
            get
            {
                return _displayedString;
            }
            set
            {
                _displayedString = value;
                _text.DisplayedString = value;
            }
        }

        public Label(String text, String fontName, uint fontSize = 24, Texture texture = null)
        {
            _text = new Text(text, AssetLoader.Instance.getFont(fontName));
            _text.CharacterSize = fontSize;
            _text.Origin = new Vector2f(0,fontSize);
            if (texture != null)
            {
                _icon = new Sprite(texture);
                _icon.Origin = new Vector2f(0, _icon.TextureRect.Height);
            }

        }

        public Label(String text, String fontName, uint fontSize, String iconTextureName)
            : this(text, fontName, fontSize, AssetLoader.Instance.getTexture(iconTextureName).Texture)
        {
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_text, states);
            if(_icon != null)
                target.Draw(_icon, states);
        }
    }
}
