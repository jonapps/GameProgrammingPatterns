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
        public event WaveEventHandler OnWavesCompleted;
        private Queue<Wave> _waves;



        public WaveManager(World world)
        {
            
            _waves = new Queue<Wave>();

            Wave w1 = new Wave();

            w1.AddEntityDef(2000, new Asteroid.AsteroidDef(60, -10, 1f, 1f, 2, 1f, 0.05f));
            w1.AddEntityDef(2000, new Asteroid.AsteroidDef(70, -10, 1f, 3f, 1, 0.5f, 0.05f));
            w1.AddEntityDef(2000, new Asteroid.AsteroidDef(70, -10, 1f, 3f, 1, 0.5f, 0.05f));
            w1.AddEntityDef(3000, new Asteroid.AsteroidDef(70, -10, 1f, 3f, 1, 0.5f, 0.05f));
            w1.AddEntityDef(4000, new Asteroid.AsteroidDef(70, -10, 1f, 3f, 1, 0.5f, 0.05f));
            w1.AddEntityDef(5000, new Asteroid.AsteroidDef(70, -10, 1f, 3f, 1, 0.5f, 0.05f));
            w1.AddEntityDef(6000, new Astronaut.AstronautDef(20, -10, 1.8f, 3.2f, 0.3f, 0.05f));
            w1.AddEntityDef(8000, new Asteroid.AsteroidDef(160, 20, -12000f, 1f, 1, 0.5f, 0.05f));
            w1.AddEntityDef(8000, new Asteroid.AsteroidDef(-5, 21, 12000f, 1f, 1, 0.5f, 0.05f));


            Wave w2 = new Wave();
            w2.AddEntityDef(0, new Astronaut.AstronautDef(20, -10, 1.8f, 3.2f, 0.3f, 0.05f));
            w2.AddEntityDef(0, new Astronaut.AstronautDef(500, -10, 2f, -5f, 0.3f, -0.06f));
            w1.AddEntityDef(3000, new Asteroid.AsteroidDef(70, -10, 1f, 3f, 1, 0.5f, 0.05f));
            w1.AddEntityDef(4000, new Asteroid.AsteroidDef(70, -10, 1f, 3f, 1, 0.5f, 0.05f));
            w1.AddEntityDef(10000, new Asteroid.AsteroidDef(70, -10, 1f, 3f, 1, 0.5f, 0.05f));
            w1.AddEntityDef(6000, new Astronaut.AstronautDef(1000, -10, -1.8f, 3.2f, 0.3f, 0.05f));


            Wave w3 = new Wave();
            w3.AddEntityDef(2000, new Asteroid.AsteroidDef(60, -10, 1f, 1f, 2, 1f, 0.05f));
            w3.AddEntityDef(2000, new Asteroid.AsteroidDef(70, -10, 1f, 3f, 1, 0.5f, 0.05f));
            w3.AddEntityDef(2000, new Asteroid.AsteroidDef(70, -10, 1f, 3f, 3, 0.5f, 0.05f));
            w3.AddEntityDef(5000, new Asteroid.AsteroidDef(70, -10, 1f, 3f, 1, 0.5f, 0.05f));
            w3.AddEntityDef(8000, new Asteroid.AsteroidDef(70, -10, 1f, 3f, 1, 0.5f, 0.05f));
            w3.AddEntityDef(10000, new Asteroid.AsteroidDef(70, -10, 1f, 3f, 1, 0.5f, 0.05f));
            w3.AddEntityDef(12000, new Astronaut.AstronautDef(20, -10, 1.8f, 3.2f, 0.3f, 0.05f));


            Wave w4 = new Wave();
            w4.AddEntityDef(2000, new Asteroid.AsteroidDef(-5, 20, 5f, 0f, 2, 1f, 0.05f));
            w4.AddEntityDef(2000, new Asteroid.AsteroidDef(-5, 40, 5f, 0f, 1, 0.5f, 0.05f));
            w4.AddEntityDef(2000, new Asteroid.AsteroidDef(-5, 10, 5f, 0.4f, 3, 0.5f, 0.05f));
            w4.AddEntityDef(5000, new Asteroid.AsteroidDef(160, 0, -3f, 0.1f, 1, 0.5f, 0.05f));
            w4.AddEntityDef(8000, new Asteroid.AsteroidDef(160, 20, -12000f, 1f, 1, 0.5f, 0.05f));
            w4.AddEntityDef(10000, new Asteroid.AsteroidDef(160, 35, -4f, 1.2f, 1, 0.5f, 0.05f));
            w4.AddEntityDef(1000, new Astronaut.AstronautDef(70, -10, 1.8f, 3.2f, 0.3f, 0.05f));

            
            _waves.Enqueue(w1);
            _waves.Enqueue(w2);
            _waves.Enqueue(w3);
            _waves.Enqueue(w4);


            this.OnWaveOver = delegate(Wave wave)
            {
                Next();
            };

            Start();

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
                GameManager.Instance.SetRocketsLeft(20);
                GameManager.Instance.SetRoundsLeft(1000);
                if (OnWaveStarted != null)
                    OnWaveStarted(_waves.Peek());
            }
        }

        public void Next()
        {
            if (HasNext())
            {
                _waves.Dequeue();
                Start();
            }
            if (!HasNext())
            {
                OnWavesCompleted(null);
            }
        }



        public void GenerateEntities()
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
        }

       

        internal void Update()
        {
            GenerateEntities();
        }
    }
}
