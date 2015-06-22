using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.EventSystem.Events
{
    class EngineEvent
    {
        public Object Data;

        public EngineEvent()
        {
            
        }

        public EngineEvent(Object data)
        {
            Data = data;
        }
    }
}
