using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Exceptions;
using JGerdesJWiemers.Game.Engine.Graphics.Screens.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine
{
    class EntityFactory
    {
        private static EntityFactory _instance;

        private World _world;
        private List<EntityHolder> _holder;

        public Entity Spawn(Entity.EntityDef def)
        {
            if (_world == null)
            {
                throw new NotInitialisatedException();
            }
            Entity e = def.Spawn(_world);
            foreach (EntityHolder eh in _holder)
            {
                eh.AddEntity(e);
            }
            return e;
        }

        public void Init(World world, List<EntityHolder> holder)
        {
            _world = world;
            _holder = holder;
        }

        public static EntityFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EntityFactory();
                }
                return _instance;
            }
        }
    }
}
