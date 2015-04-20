using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Entities
{
    abstract class CircleEntity : Entity
    {
        protected float _radius;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        public CircleEntity(float x, float y, float r) : base(x,y)
        {
            this._radius = r;
        }
    }
}
