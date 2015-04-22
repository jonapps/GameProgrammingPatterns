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
        private Text _text;

        public delegate void SelectedEventHandler(object sender, EventArgs e);
        public event SelectedEventHandler OnSelected;

        public Button(float x, float y, String label)
        {
            _text = new Text(label, AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_LIGHT));
        }

        public void Select()
        {
            if (OnSelected != null)
            {
                OnSelected(this, new EventArgs());
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
