using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.EventSystem;
using JGerdesJWiemers.Game.Engine.EventSystem.Events;
using JGerdesJWiemers.Game.Engine.Utils.Helper.LevelAssets;
using JGerdesJWiemers.Game.TowerDefence.Entities;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.Logic
{
    class WaveManager
    {
        public static readonly String EVENT_NEXT_WAVE = "waveManager.next";


        private WavesAsset _wavesAsset;
        private int _currentIndex = 0;
        private List<Enemy.Def> _enemies;
        //private World _world;
        //private Map _map;

        public WaveManager(WavesAsset w, List<Enemy.Def> enemies){
            _wavesAsset = w;
            _enemies = enemies;
            //_world = world;
            //_map = m;



            EventStream.Instance.On(EVENT_NEXT_WAVE, _Run);
        }

        private void _Run(EngineEvent eventData)
        {
            int delayMultiplayer = 0;
            WaveAsset wave = _wavesAsset.Waves[_currentIndex++];
            foreach (EnemyWavesAsset ewa in wave.Enemies)
            {
                for (int i = 0; i < ewa.Quantity; ++i)
                {
                    long delay = delayMultiplayer++ * 1000;
                    //find EnemyAssets by type
                    Enemy.Def enemyData = _enemies.Find(e => e.Name.Equals(ewa.Type));
                    if (enemyData != null)
                    {
                        EventStream.Instance.EmitDelay(Enemy.EVENT_SPAWN, new EngineEvent(enemyData), delay);
                    }
                }
            }
                
        }
    }
}
