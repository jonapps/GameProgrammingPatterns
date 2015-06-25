using FarseerPhysics;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Interfaces;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Graphics.Screens
{
    class GameScreen : Screen
    {

        public static readonly float WORLD_STEP_SIZE = 1 / 60f;

        protected List<Entity> _entities;
        protected List<Entity> _entitiesToDelete;
        protected List<Entity> _entitiesToAdd;
        protected World _world;

        public GameScreen(RenderWindow w) : base(w)
        {
            _entities = new List<Entity>();
            _entitiesToDelete = new List<Entity>();
            _entitiesToAdd = new List<Entity>();
            _world = new World(new Vector2(0, 0));
        }

        public override void Update()
        {

            for (int i = 0, c = _entities.Count; i < c; ++i)
            {
                Entity e = _entities[i];
                e.Update();
                if (e.DeleteMe)
                    _entitiesToDelete.Add(e);
            }
            foreach (Entity delE in _entitiesToDelete)
            {
                delE.DeleteFromWorld(_world);
                _entities.Remove(delE);
            }
            foreach (Entity e in _entitiesToAdd)
            {
                _entities.Add(e);
            }
            _entitiesToAdd.Clear();
            _entitiesToDelete.Clear();
                
        }

        public override void PastUpdate()
        {
            foreach(Entity e in _entities)
                e.PastUpdate();
        }

        public override void Exit()
        {
            
        }



        public override void PreDraw(float extra)
        {
            foreach(Entity e in _entities)
                e.PreDraw(extra);

            _entities.Sort((first, second) => first.Z.CompareTo(second.Z));
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            foreach(Entity e in _entities)
                target.Draw(e, states);
        }
    }
}
