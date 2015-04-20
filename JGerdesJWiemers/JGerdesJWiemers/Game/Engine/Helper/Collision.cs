using JGerdesJWiemers.Game.Engine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Helper
{
    class Collision
    {
        public static int CIRCLE_CIRCLE = 0;
        public static int RECT_CIRCLE = 1;
        public static int RECT_RECT = 2;


        private Entity _first;
        private Entity _second;

        
        public Collision(Entity a, Entity b)
        {
            this._first = a;
            this._second = b;
        }


        #region Collision getter/setter
        public Entity First
        {
            get
            {
                return _first;
            }
        }

        public Entity Second
        {
            get
            {
                return _second;
            }
        }
        #endregion
    }
}
