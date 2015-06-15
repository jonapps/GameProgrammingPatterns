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
        public static readonly float WORLD_STEP_SIZE = 1 / 60f;


        private MouseCursor _cursor;
        private Map _map;
        private World _world;
        private View _view;
        public Game(RenderWindow w)
            :base(w)
        {

            _map = new Map(24, 24, 48);
            _view = _window.GetView();

            w.SetMouseCursorVisible(false);
            _cursor = new MouseCursor(_window, _view);
            _world = new World(new Vector2(0,0));
            _drawables.Add(_map);

            _drawables.Add(new Monster(_world, _map));

            //String shaderPath = @"Assets\Shader\";
            //_shader = new Shader(null, shaderPath + "blur3.frag");

            //_shader.SetParameter("blur_radius", 0.08F);
            //_shader.SetParameter("resolution", 1280, 720);
            //_shader.SetParameter("dir", 1, 0);
            //_shader.SetParameter("radius", 0.0015f);
        }

        void w_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            //if (e.X < _window.Position.X)
            //{
            //    e.X = _window.Position.X;
            //}
            //if (e.X > (_window.Position.X + _window.Size.X))
            //{
            //    e.X = (int)_window.Size.X;
            //}
            //if (e.Y < _window.Position.Y)
            //{
            //    e.Y = _window.Position.Y;
            //}
            //if (e.Y > (_window.Position.Y + _window.Size.Y))
            //{
            //    e.Y = (int)_window.Size.Y;
            //}
            //Shape s = _map.GetTileAt(new Vector2f(e.X, e.Y));
            //if (SFML.Window.Mouse.IsButtonPressed(Mouse.Button.Right))
            //{
            //    if (s != null)
            //        s.FillColor = new Color(255, 128, 128, 120);
            //}
            //if (SFML.Window.Mouse.IsButtonPressed(Mouse.Button.Left))
            //{
            //    if (s != null)
            //        s.FillColor = new Color(128, 128, 255, 120);
            //}
            //Vector2i tile = _map.GetTileIndexAt(new Vector2f(e.X, e.Y));
            
            //Console.WriteLine("Tile@(" + e.X + "," + e.Y + "):\tx:" + tile.X + "\ty:" + tile.Y);
        }


        public override void Update()
        {
            _world.Step(WORLD_STEP_SIZE);
            base.Update();
            _cursor.Update();
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
            _window.SetView(_view);
            base.Draw(renderTarget, states);
            renderTarget.Draw(_cursor);
        }

        public override void Exit()
        {
            
        }
    }
}
