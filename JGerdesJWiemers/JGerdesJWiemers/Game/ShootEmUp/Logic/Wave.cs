using JGerdesJWiemers.Game.Engine;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Logic
{
    class Wave
    {
        private int _startTime = 0;
        private int _length = 0;
        private SortedDictionary<int, Entity[]> _generationList;

        public Wave()
        {
            _generationList = new SortedDictionary<int, Entity[]>();
        }

        public void Start()
        {
            _startTime = Game.ElapsedTime;
        }

        public List<Entity> Generate()
        {
            int currentTime = Game.ElapsedTime - _startTime;
            List<Entity> entities = new List<Entity>();
            List<int> toDelete = new List<int>();

            foreach(KeyValuePair<int, Entity[]> entry in _generationList)
            {
                if (entry.Key <= currentTime)
                {
                    entities.AddRange(entry.Value);
                }
            }
            
            //delete generated entries
            foreach (int key in toDelete)
            {
                _generationList.Remove(key);
            }

            return entities;
        }

        public bool isOver()
        {
            int currentTime = Game.ElapsedTime - _startTime;
            return currentTime >= _length && _generationList.Count == 0;
        }
    }
}
