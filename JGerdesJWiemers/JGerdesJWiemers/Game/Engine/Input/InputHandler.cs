using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Input
{
    interface InputHandler
    {
        bool OnInputEvent(string name, InputEvent e, int channel);
    }
}
