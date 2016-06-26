using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CSTest._24_Closure
{

    [TestClass]
    public class Test
    {
        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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
