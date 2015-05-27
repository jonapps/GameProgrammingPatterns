using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Input
{
    class InputManager
    {
        public delegate void OnUpEvent(float value, bool press);
        public delegate void OnDownEvent(float value, bool press);
        public delegate void OnLeftEvent(float value, bool press);
        public delegate void OnRightEvent(float value, bool press);

        public delegate void OnLandEvent(bool pressed);
        public delegate void OnTakeOffEvent(bool pressed);
        public delegate void OnShootEvent(bool pressed);
        public delegate void OnChangeWeaponEvent(bool pressed);


    }
}
