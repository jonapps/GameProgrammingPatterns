using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Input
{
    class InputMapper : InputHandler
    {
        public delegate bool EventHandler(InputEvent e, int channel);
        private Dictionary<string, List<EventHandler>> _eventHandler;

        public InputMapper()
        {
            _eventHandler = new Dictionary<string, List<EventHandler>>();
        }

        public void On(string name, EventHandler handler)
        {
            if (!_eventHandler.ContainsKey(name))
            {
                _eventHandler[name] = new List<EventHandler>();
            }
            _eventHandler[name].Add(handler);
        }

        public bool OnInputEvent(string name, InputEvent e, int channel)
        {
            if (_eventHandler.ContainsKey(name))
            {
                bool consumed = false;
                foreach (EventHandler handler in _eventHandler[name])
                {
                    if (handler(e, channel))
                    {
                        consumed = true;
                    }
                }
                return consumed;
            }
            else
            {
                return false;
            }
        }
    }
}
