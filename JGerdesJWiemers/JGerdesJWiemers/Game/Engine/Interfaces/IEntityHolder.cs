using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Interfaces
{
    interface IEntityHolder
    {
        void AddEntity(Entity e);
        List<Entity> GetEntities();
    }
}
