using System;

namespace Patterns._04_Other._03_ActiveStateMachine.ApplicationServices
{
    public abstract class Device
    {
        Action<string, string, string> _devEventMethod;

        public string DevName { get; }

        protected Device(string deviceName, Action<string, string, string> eventCallBack)
        {
            DevName = deviceName;
            _devEventMethod = eventCallBack;
        }

        public abstract void OnInit();

        public void RegisterEventCallback(Action<string, string, string> method)
        {
            _devEventMethod = method;
        }

        public void DoNotificationCallback(string name, string eventInfo, string source)
        {
            _devEventMethod.Invoke(name, eventInfo, source);
        }
    }
}
