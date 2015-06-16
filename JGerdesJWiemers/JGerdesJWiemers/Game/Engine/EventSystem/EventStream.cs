using JGerdesJWiemers.Game.Engine.EventSystem.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JGerdesJWiemers.Game.Engine.EventSystem
{
    class EventStream
    {
        public delegate void EventListener(EngineEvent eventData);

        private Dictionary<String, List<EventListener>> _events;

        private static EventStream _instance = null;

        public static EventStream Instance { 
            get
            {
                if(_instance == null)
                {
                    _instance = new EventStream();
                }
                return _instance;
            }
        }

        private EventStream()
        {
            _events = new Dictionary<string, List<EventListener>>();
        }


        public void On(string eventName, EventListener callback)
        {
            _AddListener(eventName, callback);
        }

        private void _AddListener(string eventName, EventListener callback)
        {
            if (_events.ContainsKey(eventName))
            {
                _events[eventName].Add(callback);
            }
            else
            {
                _events[eventName] = new List<EventListener>();
                _events[eventName].Add(callback);
            }
        }

        public void Emit(String eventName, EngineEvent eventData)
        {
            _NotifyAll(eventName, eventData);
        }

        private void _NotifyAll(String eventName, EngineEvent eventData)
        {
            List<EventListener> callbacks = _events[eventName];
            for (int i = 0; i < callbacks.Count; ++i)
            {
                callbacks[i].DynamicInvoke(eventData);
            } 
        }

    }
}
