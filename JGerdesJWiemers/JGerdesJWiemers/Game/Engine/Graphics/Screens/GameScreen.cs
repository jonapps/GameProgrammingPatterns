using FarseerPhysics;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Interfaces;
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
        protected List<int> _entitiesToDelete;
        protected List<Entity> _entitiesToAdd;

        public GameScreen(RenderWindow w) : base(w)
        {
            _entities = new List<Entity>();
            _entitiesToDelete = new List<int>();
            _entitiesToAdd = new List<Entity>();
            
        }

        public override void Update()
        {

            for (int i = 0, c = _entities.Count; i < c; ++i)
            {
                Entity e = _entities[i];
                e.Update();
                if (e.DeleteMe)
                    _entitiesToDelete.Add(i);
            }

            foreach (int i in _entitiesToDelete)
            {
                _entities.RemoveAt(i);
            }

            foreach (Entity e in _entitiesToAdd)
            {
                _entities.Add(e);
            }
                
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
