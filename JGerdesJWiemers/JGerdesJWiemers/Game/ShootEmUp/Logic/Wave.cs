using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.ShootEmUp.Entities;
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
        private List<Entity> _entities;

        private long _startTime = 0;
        private long _length = 0;
        private SortedDictionary<int, List<Entity.EntityDef>> _generationList;

        public Wave()
        {
            _entities = new List<Entity>();
            _generationList = new SortedDictionary<int, List<Entity.EntityDef>>();
        }

        public Wave(SortedDictionary<int, List<Entity.EntityDef>> generationList)
        {
            _entities = new List<Entity>();
            _generationList = generationList;
            _length = _generationList.Keys.Last();
        }

        public Wave(SortedDictionary<int, List<Entity.EntityDef>> generationList, long length)
        {
            _entities = new List<Entity>();
            _generationList = generationList;
            _length = length;
        }

        public void AddEntityDef(int time, Entity.EntityDef e)
        {
            if (!_generationList.ContainsKey(time))
            {
                _generationList.Add(time, new List<Entity.EntityDef>());
            }

            _generationList[time].Add(e);
        }

        public void Start()
        {
            _startTime = Game.ElapsedFrameTime;
        }


        private bool _CheckAllEntitiesDead()
        {
            bool allDead = true;
            for (int i = 0; i < _entities.Count; ++i)
            {
                if (!_entities[i].DeleteMe)
                {
                    allDead = false;
                }
            }
            return allDead;
        }

        public List<Entity> Generate()
        {
            long currentTime = Game.ElapsedTime - _startTime;
            List<Entity> entities = new List<Entity>();
            List<int> toDelete = new List<int>();

            foreach(KeyValuePair<int, List<Entity.EntityDef>> entry in _generationList)
            {
                if (entry.Key <= currentTime)
                {
                    foreach (Entity.EntityDef def in entry.Value)
                    {
                        Entity e = EntityFactory.Instance.Spawn(def);
                        entities.Add(e);
                        _entities.Add(e);
                        if(e is Asteroid){
                            (e as Asteroid).OnSplit += _AddListOfAsteroid;
                        }
                        
                    }
                    toDelete.Add(entry.Key);
                    
                }
            }
            
            //delete generated entries
            foreach (int key in toDelete)
            {
                _generationList.Remove(key);
            }

            return entities;
        }

        private void _AddListOfAsteroid(List<Asteroid> la)
        {
            _entities.AddRange(la);
            foreach (Asteroid a in la)
            {
                a.OnSplit += _AddListOfAsteroid;
            }
        }

        public bool isOver()
        {
            long currentTime = Game.ElapsedTime - _startTime;
            bool over = (currentTime >= _length && _generationList.Count == 0) && _CheckAllEntitiesDead();
            return over;
        }
    }
}
