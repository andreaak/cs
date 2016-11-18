using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Patterns.Other._03_ActiveStateMachine.ApplicationServices;
using Patterns.Other._03_ActiveStateMachine.Common;
using Patterns.Other._03_ActiveStateMachine.TelephoneStateMachine;

namespace UIApplication
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
            var phoneViewState = (TelephoneViewState) _viewManager.ViewStateConfiguration.ViewStates[viewState];
            SetValues(phoneViewState);
            CurrentViewState = phoneViewState;
        }

        //Set all values in UI corresponding to given view state
        private void SetValues(object viewState)
        {
            var phoneViewState = (TelephoneViewState) viewState;
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
                Invoke(new MethodInvoker(() => labelLine.Text = "Down"));
                Invoke(new MethodInvoker(() => labelLine.BackColor = Color.DarkSeaGreen));
            }
            else
            {
                Invoke(new MethodInvoker(() => labelLine.Text = "Up"));
                Invoke(new MethodInvoker(() => labelLine.BackColor = Color.Tomato));
            }
        }


        private void Telephone_Load(object sender, EventArgs e)
        {

        }

        private void buttonInitiateCall_Click(object sender, EventArgs e)
        {
            if(CurrentViewState != null)
                _viewManager.RaiseUICommand("ActiveExternal", "","","");
        }

        private void buttonReceiverDown_Click(object sender, EventArgs e)
        {

        }

        private void buttonReceiverUp_Click(object sender, EventArgs e)
        {

        }


    }
}
