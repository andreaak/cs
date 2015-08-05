using System;
using System.Diagnostics;

namespace Patterns.Behavioral.State.Example1.States
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
            Console.WriteLine("Нельзя заправляться с работающим двигателем");
        }

        public void TurnKey()
        {
            Console.WriteLine("Тсссс. Передышка");
            _car.SetState(_car.FullTank);
        }

        public void Drive()
        {
            TryDrive();
        }

        public void Stop()
        {
            Console.WriteLine("Я и так стою");
        }

        private void TryDrive()
        {
            if (_car.Gasoline > 0)
            {
                Console.WriteLine("Поехали!");
                _car.SetState(_car.Driving);
            }
            else
            {
                Console.WriteLine("Докатались. Бензин кончился");
                _car.SetState(_car.EmptyTank);
            }
        }
    }
}
