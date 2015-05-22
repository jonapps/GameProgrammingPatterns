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
        private Queue<Wave> _waves;

        public WaveManager(World world)
        {
            _waves = new Queue<Wave>();

            Wave w1 = new Wave();
            w1.AddEntity(2000, new Asteroid(world, 50, 10, AssetLoader.TEXTURE_ASTEROID1, 0.5f, 10f, 14f, 0.05f));
            w1.AddEntity(1000, new Astronaut(world, new Vector2f(20, -20), 1.8f, 3.2f, 0.2f));
            w1.AddEntity(3000, new Astronaut(world, new Vector2f(100, 95), 2f, -5f, -0.6f));

            _waves.Enqueue(w1);


        }

        public void Start()
        {
            _waves.Peek().Start();
        }

        public void GenerateEntities(EntityHolder holder)
        {
            if (_waves.Count > 0)
            {
                Wave currentWave = _waves.Peek();
                List<Entity> newEntities = currentWave.Generate();
                foreach (Entity e in newEntities)
                {
                    holder.AddEntity(e);
                }
                if (currentWave.isOver())
                {
                    _waves.Dequeue();
                    Console.WriteLine("Welle vorbei");
                    if (_waves.Count > 0)
                    {

                        _waves.Peek().Start();
                    }
                }
            }
        }
    }
}
