using System;
using System.Linq;
using Patterns._04_Other._03_ActiveStateMachine.Common;

namespace Patterns._04_Other._03_ActiveStateMachine.ApplicationServices
{
    public class ViewManager
    {
        public event EventHandler<StateMachineEventArgs> ViewManagerEvent;

        private string[] _viewStates;
        private string DefaultViewState;
        private IUserInterface _ui;

        public string CurrentView { get; private set; }

        public IViewStateConfiguration ViewStateConfiguration { get; set; }

        private static readonly Lazy<ViewManager> _viewManager = new Lazy<ViewManager>(() => new ViewManager());

        public static ViewManager Instance
        {
            get
            {
                return _viewManager.Value;
            }
        }

        private ViewManager()
        {
        }

        public void LoadViewStateConfiguration(IViewStateConfiguration viewStateConfiguration, IUserInterface userInterface)
        {
            ViewStateConfiguration = viewStateConfiguration;
            _viewStates = viewStateConfiguration.ViewStateList;
            _ui = userInterface;
            DefaultViewState = viewStateConfiguration.DefaultViewState;
        }

        //Handler method for state machine commands
        public void ViewCommandHandler(object sender, StateMachineEventArgs e)
        {
            //This approach assumes that there is a dedicated view state for every state machine UI command.
            try
            {
                if (_viewStates.Contains(e.EventName))
                {
                    //Convention: view command event name matches corresponding view state
                    _ui.LoadViewState(e.EventName);
                    CurrentView = e.EventName;
                    RaiseViewManagerEvent("View Manager Command", "Successfully loaded view state: " + e.EventName);
                }
                else
                {
                    RaiseViewManagerEvent("View Manager Command", "View state not found!");
                }
            }
            catch (Exception ex)
            {
                RaiseViewManagerEvent("View Manager Command - Error", ex.ToString());
                throw;
            }
        }

        //Handler method for special system events, e.g. initialization
        public void SystemEventHandler(object sender, StateMachineEventArgs e)
        {
            if (e.EventName == "OnInit")
            {
                _ui.LoadViewState(DefaultViewState);
                CurrentView = DefaultViewState;
            }
            if (e.EventName == "CompleteFailure")
            {
                _ui.LoadViewState("CompleteFailure");
            }
        }

        //Method to raise a view manager event fo logging, etc
        private void RaiseViewManagerEvent(string name, string info, StateMachineEventType eventType = StateMachineEventType.System)
        {
            var arg = new StateMachineEventArgs(name, "View manager event: " + info, eventType, "View Manager", "");
            if (ViewManagerEvent != null)
                ViewManagerEvent(this, arg);
        }

        //Send a command to another service
        public void RaiseUICommand(string command, string info, string source, string target)
        {
            var arg = new StateMachineEventArgs(command, info, StateMachineEventType.Command, source, target);
            if (ViewManagerEvent != null)
                ViewManagerEvent(this, arg);
        }
    }
}
