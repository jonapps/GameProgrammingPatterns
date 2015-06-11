using FarseerPhysics;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Interfaces;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Graphics.Screens
{
    class GameScreen : Screen
    {

        public static readonly float WORLD_STEP_SIZE = 1 / 60f;

        protected List<IDrawable> _drawables;
        protected List<int> _toDeleteEntities;
        protected View _view;

        public GameScreen(RenderWindow w) : base(w)
        {
            _drawables = new List<IDrawable>();
            _toDeleteEntities = new List<int>();
            float width = ConvertUnits.ToSimUnits(_window.Size.X);
            float height = ConvertUnits.ToSimUnits(_window.Size.Y);
            _view = new View(new Vector2f(width / 2f, height / 2f), new Vector2f(width, height));
            
        }

        public override void Update()
        {
            for (int i = 0; i < _drawables.Count; ++i)
            {
                _drawables[i].Update();
            }
        }

        public override void PastUpdate()
        {
            for (int i = 0; i < _drawables.Count; ++i)
            {
                _drawables[i].PastUpdate();
            }
        }

        public override void Exit()
        {
            
        }



        public override void PreDraw(float extra)
        {
            foreach (IDrawable e in _drawables)
            {
                e.PreDraw(extra);
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            foreach (IDrawable e in _drawables)
            {
                _window.Draw(e);
            }
        }
    }
}
