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

namespace JGerdesJWiemers.Game.TowerDefence.GameLogic
{
    class WaveManager
    {

        private WavesAsset _wavesAsset;
        private int _currentIndex = 0;
        //private World _world;
        //private Map _map;

        public WaveManager(WavesAsset w){
            _wavesAsset = w;
            //_world = world;
            //_map = m;
        }



        public void Run()
        {
            WaveAsset wave = _wavesAsset.Waves[_currentIndex++];
            foreach (EnemyWavesAsset ewa in wave.Enemies)
            {
                for (int i = 0; i < ewa.Quantity; ++i)
                {
                    EventStream.Instance.Emit(Enemy.EVENT_SPAWN, new EngineEvent(new Enemy.Def
                    {
                        Color = new Color(102, 57, 182),
                        IsFloating = true,
                        Speed = 0.5f
                    }));
                }
            }
                
        }
    }
}
