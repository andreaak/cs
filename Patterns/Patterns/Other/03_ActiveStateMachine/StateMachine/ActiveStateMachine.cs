using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Patterns.Other._03_ActiveStateMachine.Common;

namespace Patterns.Other._03_ActiveStateMachine.StateMachine
{
    public enum EngineState
    {
        Initialized,
        Running,
        Stopped,
        Paused,
    }

    public class ActiveStateMachine
    {
        public event EventHandler<StateMachineEventArgs> StateMachineEvent;

        private Task _queueWorkerTask;
        private readonly State _initialState;
        private ManualResetEvent _resumer;
        private CancellationTokenSource _tokenSource;

        public Dictionary<string, State> StateList { get; }

        public BlockingCollection<string> TriggerQueue { get; }

        public State CurrentState { get; private set; }

        public State PreviousState { get; private set; }

        public EngineState StateMachineEngine { get; private set; }

        public ActiveStateMachine(Dictionary<string, State> stateList, int queueCapacity)
        {
            StateList = stateList;
            _initialState = new State("InitialState", null, null, null);

            TriggerQueue = new BlockingCollection<string>(queueCapacity);
            InitStateMachine();
            RaiseStateMachineSystemEvent("StateMachine: Initialized", "System ready to start");
            StateMachineEngine = EngineState.Initialized;
        }

        public void InitStateMachine()
        {
            //set initial state to an unspecific initial state.
            PreviousState = _initialState;

            //Look for the default state, which is the state to begin with in SatetList
            foreach (var state in StateList)
            {
                if (state.Value.IsDefaultState)
                {
                    CurrentState = state.Value;
                    RaiseStateMachineSystemCommand("OnInit", "StateMachineInitialized");
                }
            }

            //This is synchronization object for resuming - passing true means non-blocking(signaled)
            _resumer = new ManualResetEvent(true);
        }

        public void Start()
        {
            _tokenSource = new CancellationTokenSource();
            _queueWorkerTask = Task.Factory.StartNew(QueueWorkerMethod, _tokenSource, TaskCreationOptions.LongRunning);
            StateMachineEngine = EngineState.Running;
            RaiseStateMachineSystemEvent("StateMachine: Started", "System running.");
        }

        public void Pause()
        {
            StateMachineEngine = EngineState.Paused;
            _resumer.Reset();
            RaiseStateMachineSystemEvent("StateMachine: Paused", "System waiting.");
        }

        public void Resume()
        {
            _resumer.Set();
            StateMachineEngine = EngineState.Running;
            RaiseStateMachineSystemEvent("StateMachine: Resumed", "System running.");
        }

        public void Stop()
        {
            _tokenSource.Cancel();
            _queueWorkerTask.Wait();
            _queueWorkerTask.Dispose();

            StateMachineEngine = EngineState.Stopped;
            RaiseStateMachineSystemEvent("StateMachine: Stopped", "System execution stopped.");
        }

        private void EnterTrigger(string newTrigger)
        {
            // Put trigget in queue
            try
            {
                TriggerQueue.Add(newTrigger);
            }
            catch (Exception e)
            {
                RaiseStateMachineSystemEvent("StateMachine: Error entering trigger", newTrigger + " - " + e);
                return;
            }
            RaiseStateMachineSystemEvent("StateMachine: Trigger entered", newTrigger);
        }

        private void QueueWorkerMethod(object dummy)
        {
            //Block execution until it is reset. Used to pause the state machine.
            _resumer.WaitOne();

            //Block the queue and loop through all triggers available.
            // Blocking queue guarantees FIFO and the GetConsumingEnumerable
            // automaticaly removes triggers from queue
            try
            {
                foreach (var trigger in TriggerQueue.GetConsumingEnumerable())
                {
                    if (_tokenSource.IsCancellationRequested)
                    {
                        RaiseStateMachineSystemEvent("State machine: QueueWorker", "Processing canceled!");
                        return;
                    }
                    //Compare trigger
                    foreach (var transition in CurrentState.StateTransitionList
                                                            .Where(transition => trigger == transition.Value.Trigger))
                    {
                        ExecuteTransition(transition.Value);
                    }
                    //Do not place any code here, because it will not be executed!
                    //The foreach loop keeps spinning on the queue until thread is canceled.
                }
            }
            catch (Exception ex)
            {
                RaiseStateMachineSystemEvent("State machine: QueueWorker", "Processing canceled! Exception: " + ex);
                //Create a new queue worker task. The previous one is completing right now
                Start();
            }
        }

        protected virtual void ExecuteTransition(Transition transition)
        {
            if (CurrentState.StateName != transition.SourceStateName)
            {
                string message = string.Format("Transition has wrong source state {0}, when system is in {1}",
                                                transition.SourceStateName, CurrentState.StateName);
                RaiseStateMachineSystemEvent("State machine: Default guard execute transition.", message);
                return;
            }
            if (!StateList.ContainsKey(transition.TargetStateName))
            {
                string message = string.Format("Transition has wrong target state {0}, when system is in {1}. State not in global",
                                                transition.TargetStateName, CurrentState.StateName);
                RaiseStateMachineSystemEvent("State machine: Default guard execute transition.", message);
                return;
            }

            //Self transition - just do transition without executing exit, entry actions or guards
            if (transition.SourceStateName == transition.TargetStateName)
            {
                transition.TransitionActionList.ForEach(t => t.Execute());
                return;
            }

            //Run all exit actions of the old state
            CurrentState.ExitActions.ForEach(a => a.Execute());

            //Run all guards of the transition
            transition.GuardList.ForEach(g => g.Execute());
            string info = transition.GuardList.Count + " guard actions executed!";
            RaiseStateMachineSystemEvent("State machine: ExecuteTransition.", info);

            //Run all actions of the transition
            transition.TransitionActionList.ForEach(t => t.Execute());
            info = transition.TransitionActionList.Count + " transition actions executed!";
            RaiseStateMachineSystemEvent("State machine: Begin state change", info);

            //State change
            //First resolve the target state with the help of its name
            var targetState = GetStateFromStateList(transition.TargetStateName);

            //Transition successful - Change state
            PreviousState = CurrentState;
            CurrentState = targetState;

            //Run all entry actions of new state
            foreach (var entryAction in CurrentState.EntryActions)
            {
                entryAction.Execute();
            }

            info = "Previous state: " + PreviousState.StateName + " - New state = " + CurrentState.StateName;
            RaiseStateMachineSystemEvent("State machine: State change completed successfully", info);
        }

        private State GetStateFromStateList(string targetStateName)
        {
            return StateList[targetStateName];
        }


        #region EVENT INFRASTRUCTURE

        private void RaiseStateMachineSystemEvent(string eventName, string eventInfo)
        {
            if (StateMachineEvent != null)
                StateMachineEvent(this, new StateMachineEventArgs(eventName, eventInfo, StateMachineEventType.System, "StateMachine", ""));
        }
        private void RaiseStateMachineSystemCommand(string eventName, string eventInfo)
        {
            if (StateMachineEvent != null)
                StateMachineEvent(this, new StateMachineEventArgs(eventName, eventInfo, StateMachineEventType.Command, "StateMachine", ""));
        }

        public void InternalNotificationHandler(object sender, StateMachineEventArgs e)
        {
            if (e.EventName == "CompleteFailure")
            {
                RaiseStateMachineSystemCommand("CompleteFailure", "Device: " + e.Source);
                Stop();
            }
            else
            {
                EnterTrigger(e.EventName);
            }
        }

        #endregion
    }
}
