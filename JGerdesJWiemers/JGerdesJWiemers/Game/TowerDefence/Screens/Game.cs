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
        public Game(RenderWindow w)
            :base(w)
        {
           
            _map = new Map(24, 16, 48);
            w.SetMouseCursorVisible(false);
            _cursor = new MouseCursor(_window);


            _world = new World(new Vector2(0,0));
            _drawables.Add(_map);

            _drawables.Add(new Monster(_world, _map));

            String shaderPath = @"Assets\Shader\";
            _shader = new Shader(null, shaderPath + "blur.frag");

            _shader.SetParameter("blur_radius", 0.08F);
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
            //Console.Clear();
            //Console.Clear();
            //Console.Write("\rMousePosition@ X(" + InputManager.Instance.MousePosition.X + "):\tY(" + InputManager.Instance.MousePosition.Y + ")");
            _cursor.Update();
            if(InputManager.Instance.MousePosition.X > 300)
                _shader.SetParameter("blur_radius", (InputManager.Instance.MousePosition.X-300) * 0.00005F);
            else
                _shader.SetParameter("blur_radius", 0);
        }

        public override void PastUpdate()
        {
            base.PastUpdate();
        }

        public override void Draw(SFML.Graphics.RenderTarget renderTarget, RenderStates states)
        {
            //states = new RenderStates(states);
            //states.Shader = _shader;
            base.Draw(renderTarget, states);
            renderTarget.Draw(_cursor);
        }

        public override void Exit()
        {
            
        }
    }
}
