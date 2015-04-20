using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Helper;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Utils
{
    class CollisionSolver
    {
        public CollisionSolver()
        {
            
        }

        public void solve(Collision c)
        {
            if (c.First is CircleEntity && c.Second is RectangleEntity)
            {
                _SolveCircleRectangle(c);
            }
        }

        private Collision _SolveCircleRectangle(Collision c)
        {
            for (uint i = 0; i < c.First.Shape.GetPointCount(); ++i)
            {
                Vector2f p1, p2, ps1, ps2;
            }
            return c;
        }
    }
}
