using FarseerPhysics;
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

            InputManager.Init(w);

            _window.LostFocus += delegate(object sender, EventArgs e)
            {
                if(! (_screenManager.Top() is PauseScreen))
                    _screenManager.Push(new PauseScreen(_window));
            };

            Settings.MaxPolygonVertices = 32;
            _world = new World(new Vector2(0,0));
            EntityFactory.Instance.Init(_world);

            _entities.Add(new ScrollingBackground(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_SPACE1), 0, 0, -0.02f, 0));
            _entities.Add(new ScrollingBackground(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_SPACE3), 0, 0, -0.04f, 0));
            _entities.Add(new ScrollingBackground(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_SPACE2), 0, 0, -0.06f, 0));

            _waveManager = new WaveManager(_world);
            _waveManager.OnWaveOver += delegate(Wave wave)
            {
                if (!_waveManager.HasNext())
                    Console.WriteLine("All waves are over!");
                else
                    _waveManager.Next();

            };

            Earth earth = new Earth(_world, ConvertUnits.ToSimUnits(1280 / 2f), ConvertUnits.ToSimUnits(720 / 2f), 4);
            _entities.Add(earth);
            _entities.Add(new Moon(_world, earth, 1.5f));

            _ship = new SpaceShip(10, 10, _world, this);
            _entities.Add(_ship);

            InputManager.Channel[0].OnAction2 += delegate(bool pressed)
            {
                Vector2f distance = new Vector2f(earth.Body.Position.X - _ship.Body.Position.X, earth.Body.Position.Y - _ship.Body.Position.Y);
                //only land if next to earth
                if (distance.Length2() < 10 * 10)
                {
                    //until inputs are managed by screens and not globally anymore
                    if (!(_screenManager.Top() is EarthScreen))
                    {
                        _screenManager.Push(new EarthScreen(_window));
                    }
                }
            };
        }

        public override void Update()
        {
            _world.Step(WORLD_STEP_SIZE);
            _toDeleteEntities.Clear();
            _waveManager.GenerateEntities(this);
            for (int i = 0; i < _entities.Count; ++i)
            {
                Entity e = _entities[i];
                if (e.DeleteMe)
                {
                    _toDeleteEntities.Add(i);
                }
                e.Update();
            }
            _toDeleteEntities.Sort((a, b) => b - a);
            for (int i = 0; i < _toDeleteEntities.Count; ++i)
            {
                _world.RemoveBody(_entities[_toDeleteEntities[i]].Body);
                _entities.RemoveAt(_toDeleteEntities[i]);
            }
        }
        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {

            base.Render(renderTarget, extra);
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }

        public void AddEntity(Entity e)
        {
            _entities.Add(e);
        }

    }
}
