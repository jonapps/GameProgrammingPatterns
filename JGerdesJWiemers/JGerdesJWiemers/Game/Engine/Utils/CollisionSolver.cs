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

        public Collision solve(RectangleEntity a, CircleEntity b)
        {
            return _SolveCircleRectangle(a, b);
        }















        private Collision _SolveCircleRectangle(RectangleEntity a, CircleEntity b)
        {
            Vector2f p1, p2, l1, l2;
            for (uint i = 0; i < a.LastPoints.Count(); ++i)
            {
                //todo
            }

                return new Collision(a, b);
        }
    }
}
