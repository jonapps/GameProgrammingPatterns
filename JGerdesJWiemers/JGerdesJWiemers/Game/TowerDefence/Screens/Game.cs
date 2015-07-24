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
using JGerdesJWiemers.Game.Engine.Audio;

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
        private LevelAsset _level;


        private bool _lastWave = false;
        private bool _waveStarted = false;


        private bool _viewLeft, _viewRight, _viewUp, _viewDown = false;


        public Game(RenderWindow w, LevelAsset level)
            :base(w)
        {
            _level = level;
            JGerdesJWiemers.Game.Game.ElapsedTime = 0;
            EventStream.Instance.Clear();
            _map = new Map(level.Map);
            _waveManager = new WaveManager(level.Waves, level.Enemies.Enemies);
            _uiScreen = new UiScreen(_window, _map, (ICoordsConverter)this, level);
            _clearColor = level.Info.BackgroundColor;
            ScoreManager.Instance.Energy = level.Info.StartEnergy;
            ScoreManager.Instance.Missed = 0;

            // Register Events
            EventStream.Instance.On(Enemy.EVENT_SPAWN, _SpawnEnemy);
            EventStream.Instance.On(Nuke.EVENT_SPAWN, _SpawnNuke);
            EventStream.Instance.On(Tower.EVENT_BUILD, _BuildTower);
            EventStream.Instance.On(Particle.EVENT_SPAWN, _SpawnParticle);
            EventStream.Instance.On(ScoreManager.EVENT_MISSED_CHANGED, _OnLivesChange);
            EventStream.Instance.On(WaveManager.EVENT_WAVE_STARTED, _OnWaveStart);

            _window.KeyPressed +=_window_KeyPressed;
            _window.KeyReleased += _window_KeyReleased;

            //Center view to center tile
            Vector2 center = _map.GetTileByIndex(5,5).getCenter();
            _view.Center = Map.MapToScreen(center.X, center.Y);

            _Resize(new EngineEvent(new Vector2f(_window.Size.X, _window.Size.Y)));
            AudioManager.Instance.PlayMusic("music_"+_level.Info.Name, 0.5f, _level.Info.Music);
        }



        private void _OnLivesChange(EngineEvent eventData)
        {
            if ((int)(eventData.Data) >= _level.Info.Lives)
            {
                _OnGameOver(false);
            }
        }


        private void _OnWaveStart(EngineEvent eventData)
        {
            WaveManager.WaveData data = eventData.Data as WaveManager.WaveData;
            if (data.Current == data.Total)
            {
                _lastWave = true;
            }
        }

        /// <summary>
        /// Show GameOverScreen and set Shader!
        /// </summary>
        /// <param name="win"></param>
        private void _OnGameOver(bool win)
        {
            _screenManager.PopTo(this);
            _shader = new Shader(null, @"Assets\Shader\blur.frag");
            _shader.SetParameter("blur_radius", 0f);
            GameOverScreen.Status status = win ? GameOverScreen.Status.WIN : GameOverScreen.Status.LOSE;
            _screenManager.Push(new GameOverScreen(_window, status, _shader, _level));
        }

        void _window_KeyReleased(object sender, KeyEventArgs args)
        {
            switch (args.Code)
            {
                case Keyboard.Key.Left:
                case Keyboard.Key.A:
                    _viewLeft = false;
                    break;
                case Keyboard.Key.Right:
                case Keyboard.Key.D:
                    _viewRight = false;
                    break;
                case Keyboard.Key.Up:
                case Keyboard.Key.W:
                    _viewUp = false;
                    break;
                case Keyboard.Key.Down:
                case Keyboard.Key.S:
                    _viewDown = false;
                    break;
                default:
                    break;
                    
            }
        }

        void _window_KeyPressed(object sender, KeyEventArgs args)
        {

            switch (args.Code)
            {
                case Keyboard.Key.Left:
                case Keyboard.Key.A:
                    _viewLeft = true;
                    break;
                case Keyboard.Key.Right:
                case Keyboard.Key.D:
                    _viewRight = true;
                    break;
                case Keyboard.Key.Up:
                case Keyboard.Key.W:
                    _viewUp = true;
                    break;
                case Keyboard.Key.Down:
                case Keyboard.Key.S:
                    _viewDown = true;
                    break;
                case Keyboard.Key.N:
                    if (_AllEnemiesDead())
                    {
                        _waveStarted = false;
                        EventStream.Instance.Emit(WaveManager.EVENT_NEXT_WAVE, new EngineEvent());
                    }
                    break;
                case Keyboard.Key.M:
                    _screenManager.PopTo(this);
                    _screenManager.Switch(new LevelSelector(_window));
                    EventStream.Instance.Clear();
                    break;
                case Keyboard.Key.G:
                    _OnGameOver(true);
                    break;
                case Keyboard.Key.L:
                    _OnGameOver(false);
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Spawn particle
        /// </summary>
        /// <param name="e"></param>
        private void _SpawnParticle(EngineEvent e)
        {
            Particle.Def def = e.Data as Particle.Def;
            _particlesToAdd.Add(new Particle(_world, def, this));
        }

        /// <summary>
        /// check if all enemies are dead
        /// </summary>
        /// <returns></returns>
        private bool _AllEnemiesDead()
        {
            bool allEnemiesDead = true;
            foreach (Entity e in _entities)
            {
                if (e is Enemy)
                {
                    allEnemiesDead = false;
                }
            }
            return allEnemiesDead;
        }

        public override void Create()
        {
            base.Create();
            _screenManager.Push(_uiScreen);
        }

        /// <summary>
        /// Spawn nuke
        /// </summary>
        /// <param name="eventData"></param>
        private void _SpawnNuke(EngineEvent eventData)
        {
            Nuke.Def data = (eventData.Data as Nuke.Def);
            _nukesToAdd.Add(new Nuke(_world, data));
        }

        /// <summary>
        /// Build tower on current tile if possible
        /// </summary>
        /// <param name="e"></param>
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
            ScoreManager.Instance.Energy -= def.Price;
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
            _waveStarted = true;
        }

      


        public override void Update()
        {
            _world.Step(WORLD_STEP_SIZE);
            base.Update();
            _map.Update();
            _MoveView();
            if (_viewLeft)
            {
                _MoveViewLeft();
            }
            if(_viewRight)
            {
                _MoveViewRight();
            }
            if(_viewUp)
            {
                _MoveViewUp();
            }
            if(_viewDown)
            {
                _MoveViewDown();
            }
        }

        public override void PastUpdate()
        {
            _map.PastUpdate();


            if (_AllEnemiesDead() && _lastWave && _waveStarted)
            {
                _OnGameOver(true);
            }

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
            _window.KeyPressed -= _window_KeyPressed;
            _window.KeyReleased -= _window_KeyReleased;
        }

        private void _MoveView()
        {

            Vector2i position = InputManager.Instance.MousePosition;
            if (position.X < SCROLL_DISTANCE)
            {
                _MoveViewLeft();
            }
            if (position.X > _window.Size.X - SCROLL_DISTANCE)
            {
                _MoveViewRight();
            }
            if (position.Y < SCROLL_DISTANCE)
            {
                _MoveViewUp();
            }
            if (position.Y >_window.Size.Y - SCROLL_DISTANCE)
            {
                _MoveViewDown();
            }
        }

        private void _MoveViewLeft()
        {
            _view.Move(new Vector2f(-SCROLL_SPEED, 0));
        }
        private void _MoveViewRight()
        {
            _view.Move(new Vector2f(SCROLL_SPEED, 0));
        }
        private void _MoveViewUp()
        {
            _view.Move(new Vector2f(0, -SCROLL_SPEED));
        }
        private void _MoveViewDown()
        {
            _view.Move(new Vector2f(0, SCROLL_SPEED));
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
