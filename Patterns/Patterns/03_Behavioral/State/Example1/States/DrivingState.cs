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
            Console.WriteLine("Ты что, убить нас хочешь? Нельзя заправляться на ходу");
        }

        public void TurnKey()
        {
            Console.WriteLine("На ходу ключ не трогать!");
        }

        public void Drive()
        {
            TryDrive();
        }

        public void Stop()
        {
            Console.WriteLine("Накатался? Ну постоим...");
            _car.SetState(_car.EngineStarted);
        }

        private void TryDrive()
        {
            if (_car.Gasoline > 0)
            {
                Console.WriteLine("Поехали!");
            }
            else
            {
                Console.WriteLine("Докатались. Бензин кончился");
                _car.SetState(_car.EmptyTank);
            }
        }
    }
}
