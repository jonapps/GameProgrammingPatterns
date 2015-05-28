using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Logic
{
    class GameManager
    {
        private static GameManager _instance = null;

        public delegate void GameScoreChange(int newVal);
        public event GameScoreChange OnScoreChange;
        public event GameScoreChange OnAstronautChange;
        public event GameScoreChange OnWaveChange;
        public event GameScoreChange OnEarthHealthChange;
        public event GameScoreChange OnPlayerHealthChange;
        public event GameScoreChange OnCurrentWeaponChange;
        public event GameScoreChange OnRoundsChange;
        public event GameScoreChange OnRocketsChange;


        private int _score = 0;
        private int _currentWave = 0;
        private int _astronauts = 0;
        private int _earthHealth = 0;
        private int _playerHealth = 0;
        private int _currentWeapon = 0;
        private int _roundsLeft = 0;
        private int _rocketsLeft = 0;

        public void SetPlayerHealth(int health)
        {
            _playerHealth = health;
            if (OnPlayerHealthChange != null) OnPlayerHealthChange(health);
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

        public bool NewShip()
        {
            if (_astronauts > 0)
            {
                _astronauts--;
                if (OnAstronautChange != null) OnAstronautChange(_astronauts);
                return true;
            }
            return false;
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

        public void AddScore(int score)
        {
            _score += score;
            if (OnScoreChange != null) OnScoreChange(_score);
        }

        public void ReduceScore(int reduce)
        {
            _score -= reduce;
            if (OnScoreChange != null) OnScoreChange(_score);
        }

        public void SetRoundsLeft(int rounds)
        {
            _roundsLeft = rounds;
            if (OnRoundsChange != null) OnRoundsChange(_roundsLeft);
        }

        public void SetRocketsLeft(int rockets)
        {
            _rocketsLeft = rockets;
            if (OnRocketsChange != null) OnRocketsChange(_rocketsLeft);
        }

        public void SetCurrentWeapon(int newWeapon)
        {
            _currentWeapon = newWeapon;
            if (OnCurrentWeaponChange != null) OnCurrentWeaponChange(_currentWeapon);
        }

        public int GetRoundsLeft()
        {
            return _roundsLeft;
        }

        public int GetRocketsLeft()
        {
            return _rocketsLeft;
        }


        public int ReduceRockets(int amount)
        {
            if ((_rocketsLeft - amount) > 0)
            {
                _rocketsLeft -= amount;
                if (OnRocketsChange != null) OnRocketsChange(_rocketsLeft);
                return amount;
            }
            return 0;
        }

        public int ReduceRounds(int amount)
        {
            if ((_roundsLeft - amount) > 0)
            {
                _roundsLeft -= amount;
                if (OnRocketsChange != null) OnRoundsChange(_roundsLeft);
                return amount;
            } 
            return 0;
        }


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


        public void Reset()
        {
            _score = 0;
            _currentWave = 0;
            _astronauts = 0;
            _earthHealth = 0;
            _playerHealth = 0;
            _currentWeapon = 0;
            _roundsLeft = 0;
            _rocketsLeft = 0;
            
        }

    }
}
