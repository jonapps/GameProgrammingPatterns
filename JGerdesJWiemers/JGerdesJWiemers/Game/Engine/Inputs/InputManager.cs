using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace JGerdesJWiemers.Game.Engine.Inputs
{
    class InputManager
    {

        private Hashtable _keys;
        private Hashtable _joysticks;

        private static InputManager instance;
        private InputManager()
        {
            this._keys = new Hashtable();
            this._joysticks = new Hashtable();
        }

        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputManager();
                }
                return instance;
            }
        }

    }
}
