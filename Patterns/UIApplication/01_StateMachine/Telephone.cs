using System;
using System.Drawing;
using System.Windows.Forms;
using Patterns._04_Other._03_ActiveStateMachine.ApplicationServices;
using Patterns._04_Other._03_ActiveStateMachine.Common;
using Patterns._04_Other._03_ActiveStateMachine.TelephoneStateMachine;

namespace UIApplication._01_StateMachine
{
    public partial class Telephone : Form, IUserInterface
    {
        private TelephoneStateMachine _stateMachine;
        private ViewManager _viewManager;
        private TelephoneViewStateConfiguration _viewStateConfiguration;
        private DeviceManager _deviceManager;
        private TelephoneDeviceConfiguration _telephoneDeviceConfiguration;

        public TelephoneViewState CurrentViewState { get; set; }

        public Telephone()
        {
            InitializeComponent();
        }

        public void LoadViewState(string viewState)
        {
            if (viewState == "CompleteFailure")
            {
                MessageBox.Show("Global error", "Global error");
                Invoke(new MethodInvoker(Close));
            }

            var phoneViewState = (TelephoneViewState)_viewManager.ViewStateConfiguration.ViewStates[viewState];
            SetValues(phoneViewState);
            CurrentViewState = phoneViewState;
        }

        //Set all values in UI corresponding to given view state
        private void SetValues(object viewState)
        {
            var phoneViewState = (TelephoneViewState)viewState;
            if (phoneViewState.Bell)
            {
                Invoke(new MethodInvoker(() => labelBell.Text = "Ringing"));
                Invoke(new MethodInvoker(() => labelBell.BackColor = Color.Tomato));
            }
            else
            {
                Invoke(new MethodInvoker(() => labelBell.Text = "Silent"));
                Invoke(new MethodInvoker(() => labelBell.BackColor = Color.DarkSeaGreen));
            }

            if (phoneViewState.Line)
            {
                Invoke(new MethodInvoker(() => labelLine.Text = "Active"));
                Invoke(new MethodInvoker(() => labelLine.BackColor = Color.Tomato));
            }
            else
            {
                Invoke(new MethodInvoker(() => labelLine.Text = "Off"));
                Invoke(new MethodInvoker(() => labelLine.BackColor = Color.DarkSeaGreen));
            }

            if (phoneViewState.ReceiverHungUp)
            {
                Invoke(new MethodInvoker(() => labelReceiver.Text = "Down"));
                Invoke(new MethodInvoker(() => labelReceiver.BackColor = Color.DarkSeaGreen));
            }
            else
            {
                Invoke(new MethodInvoker(() => labelReceiver.Text = "Up"));
                Invoke(new MethodInvoker(() => labelReceiver.BackColor = Color.Tomato));
            }

            if (phoneViewState.Name == "ViewErrorPhoneRings")
            {
                Invoke(new MethodInvoker(() => labelViewState.Text = "Bell is broken"));
                Invoke(new MethodInvoker(() => labelViewState.BackColor = Color.Tomato));
            }
            else
            {
                Invoke(new MethodInvoker(() => labelViewState.Text = phoneViewState.Name));
                Invoke(new MethodInvoker(() => labelViewState.BackColor = Color.White));
            }
        }

        private void Telephone_Load(object sender, EventArgs e)
        {
            //Initialize view state and ViewManager
            _viewStateConfiguration = new TelephoneViewStateConfiguration();
            _viewManager = ViewManager.Instance;
            _viewManager.LoadViewStateConfiguration(_viewStateConfiguration, this);

            //Load devices controlled by state machine into device manager
            _telephoneDeviceConfiguration = new TelephoneDeviceConfiguration();
            _deviceManager = DeviceManager.Instance;
            _deviceManager.LoadDeviceConfiguration(_telephoneDeviceConfiguration);

            //State machine
            //Initialize state machine and start state machine
            _stateMachine = new TelephoneStateMachine(new TelephoneStateMachineConfiguration());
            _stateMachine.InitStateMachine();
            _stateMachine.Start();
        }

        //Send a command to device manager 
        private void buttonInitiateCall_Click(object sender, EventArgs e)
        {
            if (CurrentViewState != null)
                _viewManager.RaiseUICommand("ActiveExternal", "Initiate call button pressed in view state:" + CurrentViewState.Name, "UI", "PhoneLine");
        }

        private void buttonReceiverDown_Click(object sender, EventArgs e)
        {
            //Never do this - the state machine must be the only place to handle state logic
            //if (CurrentViewState.Name == "ViewPhoneRings")
            //{
            //    _viewManager.RaiseUICommand("CallBlocked", "Receiver Hang Up button pressed in view state:" + CurrentViewState.Name, "UI", "Receiver");
            //}
            //else if (CurrentViewState.Name == "ViewTalking")
            //{
            //    _viewManager.RaiseUICommand("CallEnded", "Receiver Hang Up button pressed in view state:" + CurrentViewState.Name, "UI", "Receiver");
            //}
            //just send a trigger and let the state machine to do its work
            _viewManager.RaiseUICommand("OnReceiverDown", "Receiver Hang Up button pressed in view state:" + CurrentViewState.Name, "UI", "Receiver");
        }

        private void buttonReceiverUp_Click(object sender, EventArgs e)
        {
            _viewManager.RaiseUICommand("OnReceiverUp", "Receiver Lift button pressed in view state:" + CurrentViewState.Name, "UI", "Receiver");
        }
    }
}
