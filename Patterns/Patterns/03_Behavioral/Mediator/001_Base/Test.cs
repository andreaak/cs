﻿using NUnit.Framework;

namespace Patterns._03_Behavioral.Mediator._001_Base
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            var mediator = new ConcreteMediator();
            var colleague1 = new ConcreteColleague1(mediator);
            var colleague2 = new ConcreteColleague2(mediator);

            mediator.Colleague1 = colleague1;
            mediator.Colleague2 = colleague2;

            colleague1.Send("Message 1");
            colleague2.Send("Message 2");
        }
    }
}
