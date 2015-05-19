using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Graphics.Screens.Interfaces;
using JGerdesJWiemers.Game.ShootEmUp.Entities;
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
            w1.AddEntity(2000, new Asteroid(50, 10, world, 0.5f, 10f, 14f, 0.05f));

            _waves.Enqueue(w1);



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
