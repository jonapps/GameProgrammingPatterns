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
        protected List<int> _toDeleteEntities;

        public GameScreen(RenderWindow w) : base(w)
        {
            _entities = new List<Entity>();
            _toDeleteEntities = new List<int>();
            
        }

        public override void Update()
        {
            foreach(Entity e in _entities)
                e.Update();
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
