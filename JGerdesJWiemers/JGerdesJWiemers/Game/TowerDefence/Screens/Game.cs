using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Entities.Input;
using JGerdesJWiemers.Game.Engine.EventSystem;
using JGerdesJWiemers.Game.Engine.EventSystem.Events;
using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.Engine.Input;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.Engine.Utils.Helper;
using JGerdesJWiemers.Game.TowerDefence.Entities;
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
            MapAsset mapAsset = AssetLoader.Instance.LoadMap("Map3.json");
            //_map = new Map(24, 24, 48);
            _map = new Map(mapAsset);
            w.SetMouseCursorVisible(false);
            _world = new World(new Vector2(0,0));
            

            EventStream.Instance.On(Monster.EVENT_SPAWN, delegate(EngineEvent e)
            {
                for (int i = 0; i < 20; i++ )
                    _entities.Add(new Monster(_world, _map));
            });
            _window.MouseButtonPressed += _window_MouseButtonPressed;
        }

        void _window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            Tile t = _map.GetTileAtScreenPoint(_window.MapPixelToCoords(InputManager.Instance.MousePosition, _view));
            if (t != null)
            {
                t.mark();
            }
            //for (int i = 0; i < 100; i++ )
            //    EventStream.Instance.Emit(Monster.EVENT_SPAWN, new SpawnEvent());

        }

      


        public override void Update()
        {
            _world.Step(WORLD_STEP_SIZE);
            base.Update();
            _map.Update();
            _MoveView();
        }

        public override void PastUpdate()
        {
            _map.PastUpdate();
            base.PastUpdate();
        }

        public override void Draw(SFML.Graphics.RenderTarget renderTarget, RenderStates states)
        {
            renderTarget.Draw(_map, states);
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
