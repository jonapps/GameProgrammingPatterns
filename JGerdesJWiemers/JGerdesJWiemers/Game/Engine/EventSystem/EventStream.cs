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

        private List<DelayedEvent> _delayedEvents;
        private List<DelayedEvent> _executedDelayedEvents;

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
            _executedDelayedEvents = new List<DelayedEvent>();
            _delayedEvents = new List<DelayedEvent>();
            _events = new Dictionary<string, List<EventListener>>();
        }

        public void Clear()
        {
            //_delayedEvents.Clear();
            //_executedDelayedEvents.Clear();
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


        public void EmitDelay(String eventName, EngineEvent eventData, long delay)
        {
            DelayedEvent delayed = new DelayedEvent();
            delayed.Delay = delay;
            delayed.EventName = eventName;
            delayed.EventData = eventData;
            delayed.StartTime = Game.ElapsedTime;
            _delayedEvents.Add(delayed);
        }

        private void _NotifyAll(String eventName, EngineEvent eventData)
        {
            if (_events.ContainsKey(eventName))
            {
                List<EventListener> callbacks = _events[eventName];
                for (int i = 0; i < callbacks.Count; ++i)
                {
                    callbacks[i].DynamicInvoke(eventData);
                } 
            }
        }


        public void Update()
        {
            _executedDelayedEvents.Clear();
            foreach (DelayedEvent ewd in _delayedEvents)
            {
                if ((Game.ElapsedTime - ewd.StartTime) > ewd.Delay)
                {
                    _NotifyAll(ewd.EventName, ewd.EventData);
                    _executedDelayedEvents.Add(ewd);
                }
            }
            foreach (DelayedEvent ewd in _executedDelayedEvents)
            {
                _delayedEvents.Remove(ewd);
            }
            
        }

    }
}
