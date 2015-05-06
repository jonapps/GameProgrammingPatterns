using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Exceptions
{
    class NotEnoughVerticesException : Exception
    {
        public NotEnoughVerticesException()
            :base()
        {
            System.Console.WriteLine("Add min 3 Vertices");
        }
    }
}
