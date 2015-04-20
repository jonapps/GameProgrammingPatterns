using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Entities
{
    class CircleEntity : Entity
    {
        protected float _radius;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        public CircleEntity(float r)
        {
            this._radius = r;
        }
    }
}
