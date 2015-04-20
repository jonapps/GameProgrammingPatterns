using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Utils.Helper
{
    class Collision
    {

        private Entity _first;
        private Entity _secound;


        
        public Collision(Entity a, Entity b)
        {
            this._first = a;
            this._secound = b;
        }

        #region Collision getter/setter
        public Entity First
        {
            get
            {
                return _first;
            }
        }

        public Entity Secound
        {
            get
            {
                return _secound;
            }
        }
        #endregion
    }
}
