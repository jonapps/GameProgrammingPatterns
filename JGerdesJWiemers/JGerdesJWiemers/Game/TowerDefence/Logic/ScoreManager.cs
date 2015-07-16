using JGerdesJWiemers.Game.Engine.EventSystem;
using JGerdesJWiemers.Game.Engine.EventSystem.Events;
using JGerdesJWiemers.Game.TowerDefence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.Logic
{
    class ScoreManager
    {

        private static ScoreManager _instance;

        public static ScoreManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ScoreManager();
                return _instance;
            }
        }

        public static readonly String EVENT_ENERGY_CHANGED = "event.scoremanager.energychanged";
        public static readonly String EVENT_MISSED_CHANGED = "event.scoremanager.missedchanged";

        private int _energy;
        private int _missed;

        private ScoreManager()
        {
            EventStream.Instance.On(Enemy.EVENT_LOST_ENERGY, onEnemyEnergyLost);
            EventStream.Instance.On(Enemy.EVENT_DESPAWN, onEnemyDespawn);
        }

        public void onEnemyDespawn(EngineEvent e)
        {
            Missed += 1;
        }

        public void onEnemyEnergyLost(EngineEvent e)
        {
            Energy += (int)e.Data;
        }
       
        public int Energy
        {
            get { return _energy; }
            set 
            { 
                _energy = value;
                EventStream.Instance.Emit(EVENT_ENERGY_CHANGED, new EngineEvent(_energy));
            }
        }

        public int Missed
        {
            get { return _missed; }
            set 
            { 
                _missed = value;
                EventStream.Instance.Emit(EVENT_MISSED_CHANGED, new EngineEvent(_missed));
            }
        }

    }
}
