using System;
using System.Media;
using Patterns._04_Other._03_ActiveStateMachine.ApplicationServices;

namespace Patterns._04_Other._03_ActiveStateMachine.TelephoneStateMachine
{
    class DeviceBell : Device
    {
        public bool Ringing { get; set; }

        public DeviceBell(string deviceName, Action<string, string, string> eventCallBack)
            : base(deviceName, eventCallBack)
        { }

        //Initialization
        public override void OnInit()
        {
            Ringing = false;
        }

        public void Rings()
        {
            try
            {
                //Catastrofic error stoping system
                //throw new SystemException("System device fatal error");
                //
                //throw new SystemException("OnBellBroken");
                Ringing = true;
                SystemSounds.Hand.Play();
            }
            catch (Exception ex)
            {
                if (ex.Message == "OnBellBroken")
                {
                    DoNotificationCallback("OnBellBroken", ex.Message, "Bell");
                }
                else
                {
                    DoNotificationCallback("CompleteFailure", ex.Message, "Bell");
                }
            }

        }

        public void Silent()
        {
            Ringing = false;
        }
    }
}
