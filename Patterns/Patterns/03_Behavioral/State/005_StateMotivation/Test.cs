using System;
using NUnit.Framework;
using Patterns._03_Behavioral.State._005_StateMotivation.Context;

namespace Patterns._03_Behavioral.State._005_StateMotivation
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            var father = new Father();

            while (true)
            {
                Mark mark = (Mark)Enum.Parse(typeof(Mark), Console.ReadLine());

                if (mark == Mark.Two || mark == Mark.Five)
                    father.FindOut(mark);
            }
        }
    }
}
