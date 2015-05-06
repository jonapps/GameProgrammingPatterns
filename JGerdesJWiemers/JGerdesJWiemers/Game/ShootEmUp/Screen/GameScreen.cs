using FarseerPhysics;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGraphics = JGerdesJWiemers.Game.Engine.Graphics;

namespace JGerdesJWiemers.Game.ShootEmUp.Screen
{
    class GameScreen : JGraphics.Screen
    {

        public static readonly float WORLD_STEP_SIZE = 0.3f;

        private World _world;
        private List<Entity> _entities;
        private View _originalView;

        public GameScreen(RenderWindow w) : base(w)
        {
            _originalView = _window.GetView();
            ConvertUnits.SetDisplayUnitToSimUnitRatio(8f);
            float width = ConvertUnits.ToSimUnits(_window.Size.X);
            float height = ConvertUnits.ToSimUnits(_window.Size.Y);
            View view = new View(new Vector2f(width / 2f, height / 2f), new Vector2f(width, height));
            _window.SetView(view);
        }

        public override void Update()
        {

            foreach (Entity e in _entities)
            {
                e.Update();
            }

            _world.Step(WORLD_STEP_SIZE);

        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            foreach (Entity e in _entities)
            {
                e.Render(renderTarget, extra);
            }
        }

        public override void Exit()
        {
            _window.SetView(_originalView);
        }
    }
}
