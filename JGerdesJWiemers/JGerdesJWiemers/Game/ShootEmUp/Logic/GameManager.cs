using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Logic
{
    class GameManager
    {

        public int Score { get; set; }
        public int CurrentWave { get; set; }
        public int Astronauts { get; set; }


        public static GameManager Instance = null;
        private GameManager()
        {

        }

        public static GameManager GetInstance()
        {
            if (Instance == null)
            {
                new GameManager();
            }
            return Instance;
        }
    }
}
