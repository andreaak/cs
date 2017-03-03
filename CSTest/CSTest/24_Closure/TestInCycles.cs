using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CSTest._24_Closure
{

    [TestFixture]
    public class TestInCycles
    {
        [Test]
        public void TestClosure1Before()//C#4.0
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
        public void TestClosure1After()//C#5.0
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
        public void TestClosure2()
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

        //[Test]
        //public void TestClosure2AfterCompilerGenerated()
        //{
        //    List<Action> actionList = new List<Action>();
        //    foreach (int num in Enumerable.Range(1, 3))
        //    {
        //        GeneratedClass generated = new GeneratedClass();
        //        generated.i = num;
        //        // ISSUE: method pointer
        //        actionList.Add(new Action((object)generated, __methodptr(b__0)));
        //    }
        //    foreach (Action action in actionList)
        //        action();
        //}

        [Test]
        public void TestClosure3()
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

        //[Test]
        //public void TestClosure3CompilerGenerated()
        //{
        //    List<Action> actionList = new List<Action>();
        //    GeneratedClass generated = new GeneratedClass();
        //    int num;
        //    for (generated.i = 0; generated.i < 3; generated.i = num + 1)
        //    {
        //        // ISSUE: method pointer
        //        actionList.Add(new Action((object)generated, __methodptr(b__0)));
        //        num = generated.i;
        //    }
        //    foreach (Action action in actionList)
        //        action();
        //}

        [Test]
        public void TestClosure4()
        {
            var actions = new List<Action>();

            for (int i = 0; i < 3; i++)
            {
                var i1 = i;
                actions.Add(() => Debug.WriteLine(i1));
            }

            foreach (var action in actions)
            {
                action.Invoke();
            }
            /*
            0
            1
            2
            */
        }

        //[Test]
        //public void TestClosure4CompilerGenerated()
        //{
        //    List<Action> actionList = new List<Action>();
        //    for (int index = 0; index < 3; ++index)
        //    {
        //        GeneratedClass generated = new GeneratedClass();
        //        generated.i = index;
        //        // ISSUE: method pointer
        //        actionList.Add(new Action((object)generated, __methodptr(b__0)));
        //    }
        //    foreach (Action action in actionList)
        //        action();
        //}

        [Test]
        public void TestClosure5()
        {
            var names = new []{"First", "Second", "Third"};

            foreach (var name in names)
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(500);
                    Debug.WriteLine(name);
                });
            }

            Thread.Sleep(2500);

            /*
            First
            Second
            Third
            */
        }

        [Test]
        public void TestClosure6()
        {
            var names = new[] { "First", "Second", "Third" };

            for (int i = 0; i < names.Length - 1; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(500);
                    Debug.WriteLine(names[i]);
                });
            }

            Thread.Sleep(2500);

            /*
            Third
            Third
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

        //private sealed class GeneratedClass
        //{
        //    public int i;

        //    internal void b__0()
        //    {
        //        Debug.WriteLine((object)this.i);
        //    }
        //}
    }
}
