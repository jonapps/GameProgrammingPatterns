using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.Engine.Input;
using JGerdesJWiemers.Game.ShootEmUp.Entities;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Screens
{
    class Game : GameScreen
    {
        private World _world;
        private SpaceShip _ship;
        public Game(RenderWindow w)
            : base(w)
        {
            InputManager.Init(w);

            _world = new World(new Vector2(0,0));
            _entities.Add(new Earth(20, 20, _world, 5));
            _ship = new SpaceShip(20, 10, _world);
            _entities.Add(_ship);
            //_entities.Add(new CircleEntity(40, 10, 3, _world));
            //_entities.Add(new CircleEntity(45, 10, 3, _world));
            //_entities.Add(new CircleEntity(45, 25, 1, _world));
            //_entities.Add(new CircleEntity(45, 10, 1, _world));
            //_entities.Add(new CircleEntity(50, 10, 3, _world));
            //_entities.Add(new CircleEntity(55, 10, 3, _world));
            //_entities.Add(new RectangleEntity(-10, 80, 1000, 2, _world, 0,BodyType.Static));

            //_entities.Add(new Astronaut(new Vector2f(30,20), _world, 0.5f));


        }

        public override void Update()
        {
            _world.Step(WORLD_STEP_SIZE);
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            View view = renderTarget.GetView();
            view.Center = new Vector2f(_ship.Body.Position.X, _ship.Body.Position.Y);
            renderTarget.SetView(view);
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
