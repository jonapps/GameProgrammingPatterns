using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Utils;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Pong.Screens.UI
{
    class Button: IRenderable, IUpdateable
    {
        
        private Color _color;
        private Color _colorFocus;
        private Text _text;
        private bool _isFocused = false;


        public delegate void SelectedEventHandler();
        public event SelectedEventHandler OnSelected;

        public Button(float x, float y, String label)
        {
            _color = new Color(0, 0, 0, 128);
            _colorFocus = new Color(0, 0, 0, 255);
        
            _text = new Text(label, AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_LIGHT));
            _text.Color = _color;
        }

        public void Focus()
        {
            _isFocused = true;
            _text.Color = _colorFocus;
        }

        public void Blur()
        {
            _isFocused = false;
            _text.Color = _color;
        }

        public void Select()
        {
            if (OnSelected != null)
            {
                OnSelected();
            }
        }

        public void Update()
        {

        }

        public void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            renderTarget.Draw(_text);
        }
    }
}
