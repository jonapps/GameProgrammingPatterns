using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using SFML.Graphics;
using SFML.Window;  

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

        public void KeyPressed(object sender, KeyEventArgs e)
        {
            if (this._keys.ContainsKey(e.Code))
            {
                this._keys[e.Code] = true;
            }
            else
            {
                this._keys.Add(e.Code, true);
            }        
        }

        public void KeyReleased(object sender, KeyEventArgs e)
        {
            if (this._keys.ContainsKey(e.Code))
            {
                this._keys[e.Code] = false;
            }
            else
            {
                this._keys.Add(e.Code, false);
            } 
        }

        public bool IsKeyPressed(Keyboard.Key code)
        {
            if (this._keys.ContainsKey(code))
            {
                return (bool)this._keys[code];
            }
            return false;
        }

    }
}
