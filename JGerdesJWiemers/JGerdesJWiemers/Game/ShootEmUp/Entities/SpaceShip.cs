using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Entities
{
    class SpaceShip : PolygonEntity
    {
        public SpaceShip(float x, float y , World w)
            : base(x, y, null, w)
        {
            Vertices sp = new Vertices();

            sp.Add(new Vector2(0, 0));
            sp.Add(new Vector2(-15, 5));
            sp.Add(new Vector2(-6, 10));

            sp.Add(new Vector2(0, -6));
            sp.Add(new Vector2(6, 10));
            sp.Add(new Vector2(15, 5));

            


            _Create(x, y, sp, w);
        }
    }
}
