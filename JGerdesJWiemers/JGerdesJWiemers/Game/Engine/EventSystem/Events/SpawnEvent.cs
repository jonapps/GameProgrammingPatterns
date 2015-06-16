using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.EventSystem.Events
{
    class SpawnEvent : EngineEvent
    {
        public int TimeToSpawn { get; set; }
    }
}
