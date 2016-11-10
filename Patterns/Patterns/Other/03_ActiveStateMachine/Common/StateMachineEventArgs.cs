using System;

namespace Patterns.Other._03_ActiveStateMachine
{
    public enum StateMachineEventType
    {
        System,
        Command,
        Notification,
        External
    }

    class StateMachineEventArgs
    {
        public string EventName { get; }

        public string EventInfo { get; }

        public DateTime TimeStamp { get; }

        public string Source { get; }

        public string Target { get; }

        public StateMachineEventType EventType { get; }

        public StateMachineEventArgs(string eventName, string eventInfo, StateMachineEventType eventType, string source, string target)
        {
            EventName = eventName;
            EventInfo = eventInfo;
            Source = source;
            Target = target;
            EventType = eventType;
            TimeStamp = DateTime.Now;
            TimeStamp = DateTime.Now;
        }
    }
}
