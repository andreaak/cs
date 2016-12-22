using System;

namespace Patterns._04_Other._03_ActiveStateMachine.Common
{
    public enum StateMachineEventType
    {
        System,
        Command,
        Notification,
        External
    }

    public class StateMachineEventArgs
    {
        public string EventName { get; private set; }

        public string EventInfo { get; private set; }

        public DateTime TimeStamp { get; private set; }

        public string Source { get; private set; }

        public string Target { get; private set; }

        public StateMachineEventType EventType { get; private set; }

        public StateMachineEventArgs(string eventName, string eventInfo, StateMachineEventType eventType, string source, string target)
        {
            EventName = eventName;
            EventInfo = eventInfo;
            EventType = eventType;
            Source = source;
            Target = target;
            TimeStamp = DateTime.Now;
        }
    }
}
