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

        public static readonly String EVENT_ENERGY_CHANGED = "event.scoremanager.energychanged";
        public static readonly String EVENT_MISSED_CHANGED = "event.scoremanager.missedchanged";

        private int _energy;
        private int _missed;

        public ScoreManager()
        {
            EventStream.Instance.On(Enemy.EVENT_LOST_ENERGY, onEnemyEnergyLost);
            EventStream.Instance.On(Enemy.EVENT_DESPAWN, onEnemyDespawn);
        }

        public void onEnemyDespawn(EngineEvent e)
        {
            Missed += 1;
            Console.WriteLine("sm - enemyDespawn");
        }

        public void onEnemyEnergyLost(EngineEvent e)
        {
            Energy += (int)e.Data;
            Console.WriteLine("sm - enemyEnergyLost");
        }
       
        public int Energy
        {
            get { return _energy; }
            set 
            { 
                _energy = value;
                Console.WriteLine("sm - enery:" + _energy);
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
