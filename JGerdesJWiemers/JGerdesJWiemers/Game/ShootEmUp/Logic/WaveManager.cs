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

            w1.AddEntityDef(2000, new Asteroid.AsteroidDef(60, -10, 1f, 1f, 2, 1f, 0.05f));
            w1.AddEntityDef(2000, new Asteroid.AsteroidDef(70, 100, 1f, 3f, 1, 0.5f, 0.05f));

            w1.AddEntityDef(8000, new Asteroid.AsteroidDef(-20, 30, 2f, 0f, 2, 0.8f, 0.05f));
            w1.AddEntityDef(14000, new Asteroid.AsteroidDef(-20, 60, 2f, -2f, 1, 0.6f, 0.05f));
           
            w1.AddEntityDef(6000, new Astronaut.AstronautDef(20, -20, 1.8f, 3.2f, 0.3f, 0.05f));
            w1.AddEntityDef(15000, new Astronaut.AstronautDef(100, 95, 2f, -5f, 0.3f, -0.06f));

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

        public List<Entity> GenerateEntities()
        {
            List<Entity> newEntities;
            if (HasNext())
            {
                Wave currentWave = _waves.Peek();
                newEntities = currentWave.Generate();
                
                if (currentWave.isOver())
                {
                    if (OnWaveOver != null)
                        OnWaveOver(currentWave);
                }
            }
            else
            {
                newEntities = new List<Entity>();
            }
            return newEntities;
        }
    }
}
