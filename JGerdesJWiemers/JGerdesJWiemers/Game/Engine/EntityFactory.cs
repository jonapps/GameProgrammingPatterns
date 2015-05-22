using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Exceptions;
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

        public Entity Spawn(Entity.EntityDef def)
        {
            if (_world == null)
            {
                throw new NotInitialisatedException();
            }
            return def.Spawn(_world);
        }

        public void Init(World world)
        {
            _world = world;
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
