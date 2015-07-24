using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
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
using GameScreen = JGerdesJWiemers.Game.TowerDefence.Screens.Game;

namespace JGerdesJWiemers.Game.TowerDefence.Logic
{
    class WaveManager
    {
        public static readonly String EVENT_NEXT_WAVE = "waveManager.next";
        public static readonly String EVENT_WAVE_STARTED= "waveManager.started";
        

        public class WaveData
        {
            public WaveData(int c, int t)
            {
                Current = c;
                Total = t;
            }
            public int Current;
            public int Total;
        }

        private WavesAsset _wavesAsset;
        private int _currentIndex = 0;
        private List<Enemy.Def> _enemies;



        public WaveManager(WavesAsset w, List<Enemy.Def> enemies){
            _wavesAsset = w;
            _enemies = enemies;

            EventStream.Instance.On(EVENT_NEXT_WAVE, _Run);

        }






        private void _Run(EngineEvent eventData)
        {
            if (_currentIndex < _wavesAsset.Waves.Count)
            {
                
                int delayMultiplier = 0;
                WaveAsset wave = _wavesAsset.Waves[_currentIndex];
                foreach (EnemyWavesAsset ewa in wave.Enemies)
                {
                    for (int i = 0; i < ewa.Quantity; ++i)
                    {
                        long delay = delayMultiplier++ * _wavesAsset.Time;
                        //find EnemyAssets by type
                        Enemy.Def enemyData = _enemies.Find(e => e.Name.Equals(ewa.Type));
                        if (enemyData != null)
                        {
                            EventStream.Instance.EmitDelay(Enemy.EVENT_SPAWN, new EngineEvent(enemyData), delay);
                        }
                    }
                }
                ++_currentIndex;
                EventStream.Instance.Emit(EVENT_WAVE_STARTED, new EngineEvent(new WaveData(_currentIndex, _wavesAsset.Waves.Count)));
            }    
        }
    }
}
