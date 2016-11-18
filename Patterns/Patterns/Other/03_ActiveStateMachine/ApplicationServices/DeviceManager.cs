using System;
using System.Collections.Generic;
using System.Linq;
using Patterns.Other._03_ActiveStateMachine.Common;

namespace Patterns.Other._03_ActiveStateMachine.ApplicationServices
{
    public class DeviceManager
    {
        //used for logging 
        public event EventHandler<StateMachineEventArgs> DeviceManagerEvent;
        public event EventHandler<StateMachineEventArgs> DeviceManagerNotification;

        private static readonly Lazy<DeviceManager> _deviceManager = new Lazy<DeviceManager>(() => new DeviceManager());

        //List of system devices
        private Dictionary<string, object> DeviceList { get; set; }

        public static DeviceManager Instance
        {
            get
            {
                return _deviceManager.Value;
            }
        }

        private DeviceManager()
        {
            DeviceList = new Dictionary<string, object>();
        }

        public void LoadDeviceConfiguration(IDeviceConfiguration configuration)
        {
            DeviceList = configuration.Devices;
        }

        //Handler method for state machine commands
        public void DeviceCommandHandler(object sender, StateMachineEventArgs e)
        {
            //initialize
            if (e.EventType != StateMachineEventType.Command)
            {
                return;
            }

            try
            {
                if (!DeviceList.Keys.Contains(e.Target))
                    return;

                var device = DeviceList[e.Target];
                var deviceMethod = device.GetType().GetMethod(e.EventName);
                deviceMethod.Invoke(device, new object[0]);
                RaiseDeviceManagerEvent("DeviceCommand", "Successful device command: " + e.Target + " - " + e.EventName);
            }
            catch (Exception ex)
            {
                RaiseDeviceManagerEvent("DeviceCommand - Error", ex.ToString());
            }
        }

        //Handler method for special system events, e.g. initialization
        public void SystemEventHandler(object sender, StateMachineEventArgs e)
        {
            //initialize
            if (e.EventName == "OnInit" && e.EventType == StateMachineEventType.Command)
            {
                foreach (var dev in DeviceList)
                {
                    try
                    {
                        var initMethod = dev.Value.GetType().GetMethod("OnInit");
                        initMethod.Invoke(dev.Value, new object[0]);
                        RaiseDeviceManagerEvent("DeviceCommand - Initialization device", dev.Key);
                    }
                    catch (Exception ex)
                    {
                        RaiseDeviceManagerEvent("DeviceCommand - Initialization error device" + dev.Key, ex.ToString());
                    }
                }
            }

            //Notoficatin handling
            //because we use UI to trigger transitions devices would trigger normally themselves.
            // Nevertheless, tis is common, if SW userinterfaces control devices
            // View and device managers communicate on system event and use notifications to trigger state machine
            if (e.EventType == StateMachineEventType.Command)
            {
                if (e.EventName == "OnInit" || !DeviceList.ContainsKey(e.Target))
                {
                    return;
                }
                // Dispatch command to device
                DeviceCommandHandler(this, e);
            }

        }

        public void AddDevice(string name, object device)
        {
            DeviceList.Add(name, device);
            RaiseDeviceManagerEvent("Added device", name);
        }

        public void RemoveDevice(string name)
        {
            DeviceList.Remove(name);
            RaiseDeviceManagerEvent("Removed device", name);
        }

        //Method to raise a device manager event for logging, etc
        private void RaiseDeviceManagerEvent(string name, string info)
        {
            var arg = new StateMachineEventArgs(name, "Devie manager event: " + info, StateMachineEventType.System, "Device Manager", "");
            if (DeviceManagerEvent != null)
                DeviceManagerEvent(this, arg);
        }

        //Send a command from device manager to state machine
        public void RaiseDeviceManagerNotification(string command, string info, string source)
        {
            var arg = new StateMachineEventArgs(command, info, StateMachineEventType.Notification, source, "State Machine");
            var emp = DeviceManagerNotification;
            if (DeviceManagerNotification != null)
                DeviceManagerNotification(this, arg);
        }
    }
}
