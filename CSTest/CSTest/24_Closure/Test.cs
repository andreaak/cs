using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CSTest._24_Closure
{

    [TestFixture]
    public class Test
    {
        [Test]
        public void TestClosureBefore()//C#4.0
        {
            var actions = new List<Action>();

            using (var enumerator = Enumerable.Range(1, 3).GetEnumerator())
            {
                var closure = new Closure();
                while (enumerator.MoveNext())
                {
                    closure.i = enumerator.Current;
                    var action = new Action(() => Debug.WriteLine(closure.i));
                    actions.Add(action);
                }
            }
            foreach (var action in actions)
            {
                action();
            }

            /*
            3
            3
            3
            */
        }

        [Test]
        public void TestClosureAfter()//C#5.0
        {
            var actions = new List<Action>();

            using (var enumerator = Enumerable.Range(1, 3).GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var closure = new Closure();
                    closure.i = enumerator.Current;
                    var action = new Action(() => Debug.WriteLine(closure.i));
                    actions.Add(action);
                }
            }
            foreach (var action in actions)
            {
                action();
            }

            /*
            1
            2
            3
            */
        }

        [Test]
        public void TestClosureAfter2()//C#5.0
        {
            var actions = new List<Action>();
            foreach (var i in Enumerable.Range(1, 3))
            {
                actions.Add(() => Debug.WriteLine(i));
            }
            foreach (var action in actions)
            {
                action.Invoke();
            }
            /*
            1
            2
            3
            */
        }

        [Test]
        public void TestClosure4()//C#4.0
        {
            var actions = new List<Action>();

            for (int i = 0; i < 3; i++)
            {
                actions.Add(() => Debug.WriteLine(i));
            }

            foreach (var action in actions)
            {
                action.Invoke();
            }
            /*
            3
            3
            3
            */
        }

        class Closure
        {
            public int i;
            public void Action()
            {
                Debug.WriteLine(i);
            }
        }

    }
}
