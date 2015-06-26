using JGerdesJWiemers.Game.Engine.EventSystem.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.EventSystem
{
    class DelayedEvent
    {
        public long Delay { get; set; }
        public long StartTime { get; set; }
        public String EventName { get; set; }
        public EngineEvent EventData { get; set; }
    }
}
