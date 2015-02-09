using System;
using System.Diagnostics;

namespace Patterns.Behavioral.State.Example1.States
{
    class DrivingState : IState
    {
        private readonly Car _car;

        public DrivingState(Car car)
        {
            _car = car;
        }

        public void FillTank()
        {
            Debug.WriteLine("Ты что, убить нас хочешь? Нельзя заправляться на ходу");
        }

        public void TurnKey()
        {
            Debug.WriteLine("На ходу ключ не трогать!");
        }

        public void Drive()
        {
            TryDrive();
        }

        public void Stop()
        {
            Debug.WriteLine("Накатался? Ну постоим...");
            _car.SetState(_car.EngineStarted);
        }

        private void TryDrive()
        {
            if (_car.Gasoline > 0)
            {
                Debug.WriteLine("Поехали!");
            }
            else
            {
                Debug.WriteLine("Докатались. Бензин кончился");
                _car.SetState(_car.EmptyTank);
            }
        }
    }
}
