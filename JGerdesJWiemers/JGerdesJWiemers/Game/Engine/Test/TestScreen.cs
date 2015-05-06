using FarseerPhysics;
using FarseerPhysics.Collision;
using FShapes = FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using JGerdesJWiemers.Game.Engine.Graphics;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine;


namespace JGerdesJWiemers.Game
{
    class TestScreen : GameScreen
    {
        private World _world;

        public TestScreen(RenderWindow w):base(w)
        {
            _world = new World(new Vector2(0,9.81f));
            _entities.Add(new RectangleEntity(10, 12, 10, 10, _world, 1));
            _entities.Add(new RectangleEntity(10, 50, 10, 10, _world, 0, BodyType.Static));
        }

        public override void Update()
        {
            _world.Step(WORLD_STEP_SIZE);
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            for (int i = 0; i < _entities.Count; ++i)
            {
                _entities[i].Render(renderTarget, extra);
            }
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }


        
    }
}
