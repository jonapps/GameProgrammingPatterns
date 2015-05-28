using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Logic
{
    class GameManager
    {

        public delegate void GameScoreChange(int newVal);
        public event GameScoreChange OnScoreChange;
        public event GameScoreChange OnAstronautChange;
        public event GameScoreChange OnWaveChange;
        public event GameScoreChange OnEarthHealthChange;
        public event GameScoreChange OnPlayerHealthChange;

        private int _score = 0;
        private int _currentWave = 0;
        private int _astronauts = 0;
        private int _earthHealth = 0;
        private int _playerHealth = 0;


        public void SetPlayerHealth(int health)
        {
            _playerHealth = health;
            if (OnPlayerHealthChange != null) OnPlayerHealthChange(health);
        }

        public void AddScore(int score)
        {
            _score += score;
            if (OnScoreChange != null) OnScoreChange(_score);
        }

        public void SetWave(int wave)
        {
            _currentWave = wave;
            if (OnWaveChange != null) OnWaveChange(_currentWave);
        }

        public void AddAstronauts(int astronauts)
        {
            _astronauts += astronauts;
            if (OnAstronautChange != null) OnAstronautChange(_astronauts);
        }

        public void SetEarthHealth(int health)
        {
            _earthHealth = health;
            if (OnEarthHealthChange != null) OnEarthHealthChange(_earthHealth);
        }

        public int GetScore()
        {
            return _score;
        }


        private static GameManager _instance = null;
        private GameManager()
        {

        }
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }
    }
}
