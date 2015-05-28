using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.Engine.Graphics.Screens.Interfaces;
using JGerdesJWiemers.Game.Engine.Input;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.ShootEmUp.Entities;
using JGerdesJWiemers.Game.ShootEmUp.Logic;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JGerdesJWiemers.Game.ShootEmUp
{
    public static class EntityCategory
    {
        public static Category SpaceShip = Category.Cat1;
        public static Category Bullet = Category.Cat2;
        public static Category Asteroit = Category.Cat3;
        public static Category Earth = Category.Cat4;
        public static Category Moon = Category.Cat5;
        public static Category Astronaut = Category.Cat6;
    }
}

namespace JGerdesJWiemers.Game.ShootEmUp.Screens
{
    class Game : GameScreen, EntityHolder
    {
        private World _world;
        private WaveManager _waveManager;
        private SpaceShip _ship;
        public Game(RenderWindow w)
            : base(w) 
        {
            // reset all globals 
            GameManager.Instance.Reset();

            _window.LostFocus += delegate(object sender, EventArgs e)
            {
                if(! (_screenManager.Top() is PauseScreen))
                    _screenManager.Push(new PauseScreen(_window));
            };
            _world = new World(new Vector2(0,0));

            _waveManager = new WaveManager(_world);
            EntityFactory.Instance.Init(_world, new List<EntityHolder> {this});

            _entities.Add(new ScrollingBackground(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_SPACE1), 0, 0, -0.02f, 0));
            _entities.Add(new ScrollingBackground(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_SPACE3), 0, 0, -0.04f, 0));
            _entities.Add(new ScrollingBackground(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_SPACE2), 0, 0, -0.06f, 0));

            _CreateSpaceShip();

            _waveManager.OnWavesCompleted += delegate(Wave wave)
            {
                GameOver();
            };

            Earth earth = new Earth(_world, ConvertUnits.ToSimUnits(1280 / 2f), ConvertUnits.ToSimUnits(600), 4);
            earth.OnEarthDestroyed += delegate()
            {
                GameOver();
            };
            _entities.Add(earth);
            //_entities.Add(new Moon(_world, earth, 1.5f));
            _input.On("land", delegate(InputEvent e, int channel){
                Vector2f distance = new Vector2f(earth.Body.Position.X - _ship.Body.Position.X, earth.Body.Position.Y - _ship.Body.Position.Y);
                //only land if next to earth
                if (distance.Length2() < 10 * 10)
                    _screenManager.Push(new EarthScreen(_window));
                return true;
            });

            _input.On("return", delegate(InputEvent e, int channel)
            {
                if (!(_screenManager.Top() is PauseScreen))
                    _screenManager.Push(new PauseScreen(_window));
                return true;
            });
            
        }

        private void _CreateSpaceShip()
        {
            _ship = new SpaceShip(70, 70, _world, this);
            _ship.OnDestroy += delegate()
            {
                Console.WriteLine("spaceship down");
                if (GameManager.Instance.NewShip())
                {
                    _CreateSpaceShip();
                }
                else
                {
                    GameOver();
                }
            };

            _entities.Add(_ship);
        }

        public override void Create()
        {
            _screenManager.Push(new UiScreen(_window));
        }


        public void GameOver()
        {
            if (!(_screenManager.Top() is GameOverScreen))
            {
                _screenManager.PopTo(this);
                _screenManager.Switch(new GameOverScreen(_window));
            }

            
        }

        private void _CheckEntitiesOffScreen(Entity e)
        {
            //AABB aabb = new AABB();
            //aabb.UpperBound = new Vector2(ConvertUnits.ToSimUnits(-100), ConvertUnits.ToSimUnits(-100));
            //aabb.LowerBound = new Vector2(ConvertUnits.ToSimUnits(1380), ConvertUnits.ToSimUnits(860));
            //_world.QueryAABB()
            if (e.Body == null)
            {
                return;
            }
            float x = ConvertUnits.ToDisplayUnits(e.Body.Position.X);
            float y = ConvertUnits.ToDisplayUnits(e.Body.Position.Y);
            if ((x < -100 || x > 1380) || (y < -100 || y > 860))
            {
                e.DeleteMe = true;
            }
        }

        public override void Update()
        {

            _world.Step(WORLD_STEP_SIZE);
            _waveManager.Update();
            for (int i = 0; i < _entities.Count; ++i)
            {
                Entity e = _entities[i];
                _CheckEntitiesOffScreen(e);
                if (e.DeleteMe)
                {
                    _toDeleteEntities.Add(i);
                }
                else
                {
                    e.Update();
                }
                
            }
           
        }

        public override void PastUpdate()
        {
            for (int i = 0; i < _entities.Count; ++i)
            {
                if (!_entities[i].DeleteMe)
                {
                    _entities[i].PastUpdate();
                }
            }

            _toDeleteEntities.Sort((a, b) => b - a);
            for (int i = 0; i < _toDeleteEntities.Count; ++i)
            {
                _world.RemoveBody(_entities[_toDeleteEntities[i]].Body);
                _entities.RemoveAt(_toDeleteEntities[i]);
            }
            _toDeleteEntities.Clear();
        }


        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {

            base.Render(renderTarget, extra);
        }

       

        public override void Exit()
        {
            //TODO
        }

        public void AddEntity(Entity e)
        {
            _entities.Add(e);
        }

        public override bool OnInputEvent(string name, InputEvent e, int channel)
        {
            if (!base.OnInputEvent(name, e, channel))
            {
                return _ship.OnInputEvent(name, e, channel);
            }
            return false;
        }

    }
}
