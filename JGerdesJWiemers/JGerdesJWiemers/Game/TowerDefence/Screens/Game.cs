using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Entities.Input;
using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.Engine.Input;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.Screens
{
    class Game : GameScreen
    {

        public float SCROLL_SPEED = 10;
        public float SCROLL_DISTANCE = 30;

        private Map _map;
        private World _world;
        public Game(RenderWindow w)
            :base(w)
        {

            _map = new Map(24, 24, 48);
            //_view = _window.GetView();


            w.SetMouseCursorVisible(false);
            _world = new World(new Vector2(0,0));
            _drawables.Add(_map);

            _drawables.Add(new Monster(_world, _map));

            //String shaderPath = @"Assets\Shader\";
            //_shader = new Shader(null, shaderPath + "blur3.frag");

            //_shader.SetParameter("blur_radius", 0.08F);
            //_shader.SetParameter("resolution", 1280, 720);
            //_shader.SetParameter("dir", 1, 0);
            //_shader.SetParameter("radius", 0.0015f);
            _window.MouseButtonPressed += _window_MouseButtonPressed;
        }

        void _window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            Tile t = _map.GetTileAtScreenPoint(_window.MapPixelToCoords(InputManager.Instance.MousePosition, _view));
            if (t != null)
            {
                t.mark();
            }
        }

      


        public override void Update()
        {
            _world.Step(WORLD_STEP_SIZE);
            base.Update();
            _MoveView();
            //if(InputManager.Instance.MousePosition.X > 300)
            //    _shader.SetParameter("radius", (InputManager.Instance.MousePosition.X-300) * 0.00001F);
            //else
            //    _shader.SetParameter("radius", 0);
        }

        public override void PastUpdate()
        {
            base.PastUpdate();
        }

        public override void Draw(SFML.Graphics.RenderTarget renderTarget, RenderStates states)
        {
            base.Draw(renderTarget, states);
        }

        public override void Exit()
        {
            
        }

        private void _MoveView()
        {

            Vector2i position = InputManager.Instance.MousePosition;
            if (position.X < SCROLL_DISTANCE)
            {
                _view.Move(new Vector2f(-SCROLL_SPEED, 0));
            }
            if (position.X > _window.Size.X - SCROLL_DISTANCE)
            {
                _view.Move(new Vector2f(SCROLL_SPEED, 0));
            }
            if (position.Y < SCROLL_DISTANCE)
            {
                _view.Move(new Vector2f(0, -SCROLL_SPEED));
            }
            if (position.Y >_window.Size.Y - SCROLL_DISTANCE)
            {
                _view.Move(new Vector2f(0, SCROLL_SPEED));
            }
        }
    }
}
