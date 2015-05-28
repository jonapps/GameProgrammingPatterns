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
