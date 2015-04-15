using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using JGerdesJWiemers.Game.Engine.Graphics;
using SFML.Window;

namespace JGerdesJWiemers.Game.Pong.Screens
{
    class StartScreen: Screen
    {

        public StartScreen(Window w)
            : base(w)
        {
            _window.KeyPressed+= this._ProcessKeyInput;
        }

        private _ProcessKeyInput(Object sender, KeyEventArgs e){

        }

        public override void Update()
        {
            
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
           
        }
    }
}
