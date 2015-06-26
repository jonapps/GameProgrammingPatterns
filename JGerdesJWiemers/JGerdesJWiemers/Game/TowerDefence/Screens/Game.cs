using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Entities.Input;
using JGerdesJWiemers.Game.Engine.EventSystem;
using JGerdesJWiemers.Game.Engine.EventSystem.Events;
using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.Engine.Input;
using JGerdesJWiemers.Game.Engine.Interfaces;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.Engine.Utils.Helper;
using JGerdesJWiemers.Game.TowerDefence.Entities;
using JGerdesJWiemers.Game.TowerDefence.Logic;
using JGerdesJWiemers.Game.TowerDefence.Logic.AI;
using JGerdesJWiemers.Game.TowerDefence.Tiles;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game;
using JGerdesJWiemers.Game.Engine.Utils.Helper.LevelAssets;
using JGerdesJWiemers.Game.TowerDefence.Logic;

namespace JGerdesJWiemers.Game.TowerDefence.Screens
{

    public static class EntityCategory
    {
        public static Category Monster = Category.Cat1;
        public static Category Tower = Category.Cat2;
        public static Category Nuke = Category.Cat3;
        public static Category Particle = Category.Cat4;
    }

    class Game : GameScreen, IEntityHolder, ICoordsConverter
    {

        public float SCROLL_SPEED = 10;
        public float SCROLL_DISTANCE = 30;

        private Map _map;
        private WaveManager _waveManager;
        private UiScreen _uiScreen;
        
        public Game(RenderWindow w)
            :base(w)
        {
            
            AssetLoader.Instance.LoadEnemyTextures();
            List<LevelAsset> levels = AssetLoader.Instance.ReadLevels();



            // do this somewhere else    --------------------------------------------
            LevelAsset level = levels.First();
            _map = new Map(level.Map);
            _waveManager = new WaveManager(level.Waves, level.Enemies.Enemies);
            _uiScreen = new UiScreen(_window, _map, level.Tower, (ICoordsConverter)this);
            _clearColor = level.Info.BackgroundColor;
            // do this somewhere else    --------------------------------------------

            


            w.SetMouseCursorVisible(false);
            EventStream.Instance.On(Enemy.EVENT_SPAWN, _SpawnEnemy);
            EventStream.Instance.On(Nuke.EVENT_SPAWN, _SpawnNuke);
            EventStream.Instance.On(Tower.EVENT_BUILD, _BuildTower);
            EventStream.Instance.On(Particle.EVENT_SPAWN, _SpawnParticle);

            _window.KeyPressed += delegate(object sender, KeyEventArgs args)
            {
                //if(args.Code == Keyboard.Key.G)
                    //EventStream.Instance.Emit(Enemy.EVENT_SPAWN, new SpawnEvent());


                if (args.Code == Keyboard.Key.N)
                    EventStream.Instance.Emit(WaveManager.EVENT_NEXT_WAVE, new EngineEvent());

                //if (args.Code == Keyboard.Key.P)
                //{
                    
                
                //}
                   
            };


            //Center view to center tile
            Vector2 center = _map.GetTileByIndex(5,5).getCenter();
            _view.Center = Map.MapToScreen(center.X, center.Y);
        }

        private void _SpawnParticle(EngineEvent e)
        {
            Particle.Def def = e.Data as Particle.Def;
            _entitiesToAdd.Add(new Particle(_world, def));
        }

        public override void Create()
        {
            base.Create();
            _screenManager.Push(_uiScreen);
        }

        private void _SpawnNuke(EngineEvent eventData)
        {
            Nuke.Def data = (eventData.Data as Nuke.Def);
            _entitiesToAdd.Add(new Nuke(_world, data));
        }

        void _BuildTower(EngineEvent e)
        {
            Tower.Def def = e.Data as Tower.Def;
            Tower tower = new Tower(_world, def, this);
            _entities.Add(tower);

            Tile t = _map.GetTileAtMapPoint(def.Position.X, def.Position.Y);
            if (t != null)
            {
                t.Occupier = tower;
            }
        }

        /// <summary>
        /// Spawns a Monster at the first spawn point
        /// </summary>
        /// <param name="e"></param>
        private void _SpawnEnemy(EngineEvent e)
        {
            Enemy.Def def = e.Data as Enemy.Def;
            
            foreach (Tile pos in _map.GetSpawnTiles())
            {
                def.Position = pos.getCenter();
                _entitiesToAdd.Add(new Enemy(_world, def, new FollowRoadAI(_map)));
            }
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

        public override void PreDraw(float extra)
        {
            base.PreDraw(extra);
            _map.PreDraw(extra);
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

       
        public void AddEntity(Entity e)
        {
            _entities.Add(e);
        }


        public List<Entity> GetEntities()
        {
            return _entities;
        }

        public Vector2f MapPixelToCoords(Vector2i pixelPoint)
        {
            return _window.MapPixelToCoords(pixelPoint, _view);
        }


        public Vector2i MapCoordsToPixel(Vector2f coordsPoint)
        {
            return _window.MapCoordsToPixel(coordsPoint, _view);
        }
    }
}
