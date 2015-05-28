using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.Engine.Utils;
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
        private Sprite _bg;
        private View _view;

        public UiScreen(RenderWindow w)
            :base(w)
        {
            _bg = new Sprite(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_UI).Texture);
            _bg.Origin = new Vector2f(0, _bg.GetLocalBounds().Height);
            _bg.Position = new Vector2f(0, 720);
            Vector2f size = new Vector2f(w.Size.X, w.Size.Y);
            _view = new View(size / 2f, size);
        }

        public override void Update()
        {
            
        }

        public override void PastUpdate()
        {
            
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            renderTarget.SetView(_view);
            renderTarget.Draw(_bg);
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
