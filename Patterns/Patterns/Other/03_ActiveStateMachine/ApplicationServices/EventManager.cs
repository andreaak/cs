using System;
using System.Collections.Generic;

namespace Patterns.Other._03_ActiveStateMachine.ApplicationServices
{
    class EventManager
    {
        //used for logging 
        public event EventHandler<StateMachineEventArgs> EventManagerEvent;

        //Collection of registered events
        private readonly Dictionary<string, object> EventList;

        private static readonly Lazy<EventManager> _eventManager = new Lazy<EventManager>(() => new EventManager());

        public static EventManager Instance
        {
            get
            {
                return _eventManager.Value;
            }
        }

        private EventManager()
        {
            EventList = new Dictionary<string, object>();
        }

        public void RegisterEvent(string eventName, object source)
        {
            EventList.Add(eventName, source);
        }

        //Subscription method maps handler method in a sink object to an event of the source object.
        public bool SubscribeEvent(string eventName, string handlerMethodName, object sink)
        {
            try
            {
                var evt = EventList[eventName];
                var eventInfo = evt.GetType().GetEvent(eventName);
                var methodInfo = sink.GetType().GetMethod(handlerMethodName);
                //Create new delegate mapping event to handler
                Delegate handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, sink, methodInfo);
                eventInfo.AddEventHandler(evt, handler);
                return true;
            }
            catch (Exception)
            {
                var message = "Exception while subscribing to handler. Event:" + eventName + " - Handler: " +
                              handlerMethodName;
                RaiseEventManagerEvent("EventManagerSystemEvent", message, StateMachineEventType.System);
                return false;
            }
        }

        private void RaiseEventManagerEvent(string eventName, string eventInfo, StateMachineEventType eventType)
        {
            var arg = new StateMachineEventArgs(eventName, eventInfo, eventType, "EventManager", "");
            if (EventManagerEvent != null)
                EventManagerEvent(this, arg);
        }
    }
}
