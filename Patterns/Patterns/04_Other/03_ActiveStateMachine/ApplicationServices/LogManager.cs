using System;
using System.Diagnostics;
using Patterns._04_Other._03_ActiveStateMachine.Common;

namespace Patterns._04_Other._03_ActiveStateMachine.ApplicationServices
{
    public class LogManager
    {
        private static readonly Lazy<LogManager> _logger = new Lazy<LogManager>(() => new LogManager());

        public static LogManager Instance
        {
            get
            {
                return _logger.Value;
            }
        }

        private LogManager()
        {
        }

        //Log infos to debug window
        public void LogEventHandler(object sender, StateMachineEventArgs e)
        {
            //Log system events
            if (e.EventType != StateMachineEventType.Notification)
            {
                Debug.Print(e.TimeStamp + " SystemEvent: " + e.EventName + " - Info: " + e.EventInfo
                    + " - StateMachineArgumentType: " + e.EventType + " - Source: " + e.Source + " - Target: " + e.Target);
            }
            //Log state machine notifications
            else
            {
                Debug.Print(e.TimeStamp + " Notification: " + e.EventName + " - Info: " + e.EventInfo
                    + " - StateMachineArgumentType: " + e.EventType + " - Source: " + e.Source + " - Target: " + e.Target);
            }
        }
    }
}
