using System;
using System.Diagnostics;
using Patterns.Other._03_ActiveStateMachine.Common;

namespace Patterns.Other._03_ActiveStateMachine.ApplicationServices
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

        public void LoadViewStateConfiguration(IDeviceConfiguration configuration)
        {
            //DeviceList = configuration.Devices;
        }

        //Log info to debug window
        public void DeviceCommandHandler(object sender, StateMachineEventArgs e)
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
