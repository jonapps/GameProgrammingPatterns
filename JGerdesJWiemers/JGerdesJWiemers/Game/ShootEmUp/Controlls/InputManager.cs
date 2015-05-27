using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Controlls
{
    class InputManager
    {
        public delegate void OnUp(float value, bool pressed);
        public delegate void OnDown(float value, bool pressed);
        public delegate void OnLeft(float value, bool pressed);
        public delegate void OnRight(float value, bool pressed);
        public delegate void OnLand(float value, bool pressed);
        public delegate void OnTakeoff(float value, bool pressed);
        public delegate void OnShoot(float value, bool pressed);
        //public delegate void OnDown(float value, bool pressed);
        //public delegate void OnDown(float value, bool pressed);
        //public delegate void OnDown(float value, bool pressed);
    }
}
