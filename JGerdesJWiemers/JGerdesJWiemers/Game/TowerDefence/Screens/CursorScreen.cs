using JGerdesJWiemers.Game.Engine.Entities.Input;
using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.Screens
{
    class CursorScreen : Screen
    {
        private MouseCursor _cursor;

        public CursorScreen(RenderWindow w)
        :base(w){
            _cursor = new MouseCursor(w);
        }
        public override void Exit()
        {
            //throw new NotImplementedException();
        }

        public override void Update()
        {
            _cursor.Update();
        }

        public override void PastUpdate()
        {
            _cursor.PastUpdate();
        }

        public override void PreDraw(float extra)
        {
            _cursor.PreDraw(extra);
        }

        public override void Draw(SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states)
        {
            target.Draw(_cursor, states);
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
