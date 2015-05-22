using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Graphics.Screens.Interfaces;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.ShootEmUp.Entities;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Logic
{
    class WaveManager
    {

        public delegate void WaveEventHandler(Wave wave);
        public event WaveEventHandler OnWaveStarted;
        public event WaveEventHandler OnWaveOver;

        private Queue<Wave> _waves;


        public WaveManager(World world)
        {
            _waves = new Queue<Wave>();

            Wave w1 = new Wave();

            w1.AddEntityDef(2000, new Asteroid.AsteroidDef(50, 10, 10f, 14f, 0.5f, 0.05f));
            w1.AddEntityDef(1000, new Astronaut.AstronautDef(20, -20, 1.8f, 3.2f, 0.5f, 0.05f));
            w1.AddEntityDef(1000, new Astronaut.AstronautDef(100, 95, 2f, -5f, 0.5f, -0.06f));

            _waves.Enqueue(w1);


        }

        public bool HasNext()
        {
            return _waves.Count > 0;
        }

        public void Start()
        {
            if (HasNext())
            {
                _waves.Peek().Start();

                if (OnWaveStarted != null)
                    OnWaveStarted(_waves.Peek());
            }
        }

        public void Next()
        {
            if (HasNext())
            {
                _waves.Dequeue();
            }

            Start();
        }

        public void GenerateEntities(EntityHolder holder)
        {
            if (HasNext())
            {
                Wave currentWave = _waves.Peek();
                List<Entity> newEntities = currentWave.Generate();
                foreach (Entity e in newEntities)
                {
                    holder.AddEntity(e);
                }
                if (currentWave.isOver())
                {
                    if (OnWaveOver != null)
                        OnWaveOver(currentWave);
                    
                }
            }
        }
    }
}
