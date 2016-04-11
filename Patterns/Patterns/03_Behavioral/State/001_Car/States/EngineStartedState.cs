using System;
using System.Diagnostics;

namespace Patterns.Behavioral.State._001_Car.States
{
    class EngineStartedState : IState
    {
        private readonly Car _car;

        public EngineStartedState(Car car)
        {
            _car = car;
        }

        public void FillTank()
        {
            Debug.WriteLine("Нельзя заправляться с работающим двигателем");
        }

        public void TurnKey()
        {
            Debug.WriteLine("Тсссс. Передышка");
            _car.SetState(_car.FullTank);
        }

        public void Drive()
        {
            TryDrive();
        }

        public void Stop()
        {
            Debug.WriteLine("Я и так стою");
        }

        private void TryDrive()
        {
            if (_car.Gasoline > 0)
            {
                Debug.WriteLine("Поехали!");
                _car.SetState(_car.Driving);
            }
            else
            {
                Debug.WriteLine("Докатались. Бензин кончился");
                _car.SetState(_car.EmptyTank);
            }
        }
    }
}
