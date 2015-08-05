using System;
using Patterns.Behavioral.State.Example1.States;

namespace Patterns.Behavioral.State.Example1
{
    class Car
    {
        public int Gasoline { get; private set; }

        public EmptyTankState EmptyTank { get; private set; }
        public FullTankState FullTank { get; private set; }
        public EngineStartedState EngineStarted { get; private set; }
        public DrivingState Driving { get; private set; }

        private IState _currentState;

        public Car()
        {
            EmptyTank = new EmptyTankState(this);
            FullTank = new FullTankState(this);
            EngineStarted = new EngineStartedState(this);
            Driving = new DrivingState(this);

            _currentState = EmptyTank;
            Gasoline = 0;
        }

        public void FillTank()
        {
            Gasoline = 70;
            _currentState.FillTank();
        }

        public void TurnKey()
        {
            _currentState.TurnKey();
        }

        public void Drive()
        {
            _currentState.Drive();
            Gasoline -= 10;
        }

        public void Stop()
        {
            _currentState.Stop();
        }

        public void SetState(IState state)
        {
            _currentState = state;
        }
    }
}
