using System;
using System.Diagnostics;

namespace Patterns.Behavioral.State.Example1.States
{
    class FullTankState : IState
    {
        private readonly Car _car;

        public FullTankState(Car car)
        {
            _car = car;
        }

        public void FillTank()
        {
            Console.WriteLine("В меня столько не влезет");
        }

        public void TurnKey()
        {
            Console.WriteLine("Дрын дын дын дын дын тррррррррр");
            _car.SetState(_car.EngineStarted);
        }

        public void Drive()
        {
            Console.WriteLine("Сначала заведи меня");
        }

        public void Stop()
        {
            Console.WriteLine("Я и так стою");
        }
    }
}
