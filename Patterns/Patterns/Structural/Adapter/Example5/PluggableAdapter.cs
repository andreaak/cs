using System;
using System.Collections.Generic;

// Сменный адаптер.

namespace Patterns.Structural.Adapter.Example5
{
    // ---------- Существующие в системе типы ----------

    interface ITarget
    {
        void Request();
    }

    class Target : ITarget
    {
        void ITarget.Request()
        {
            Console.WriteLine("Target.Request");
        }
    }

    // ---------- Классы требующие адаптации ----------

    class AdapteeA
    {
        public void SpecificRequestA()
        {
            Console.WriteLine("AdapteeA.SpecificRequestA");
        }
    }

    class AdapteeB
    {
        public void SpecificRequestB()
        {
            Console.WriteLine("AdapteeB.SpecificRequestB");
        }
    }

    // ---------- Механизмы адаптации ----------

    enum Adaptee
    {
        A, B
    }

    // Сменный адаптер может адаптировать любое количество Adaptee.
    class PluggableAdapter : ITarget
    {
        Action request;

        public PluggableAdapter(Adaptee adaptee)
        {
            switch (adaptee)
            {
                case Adaptee.A:
                    {
                        request = new AdapteeA().SpecificRequestA;
                    }
                    break;
                case Adaptee.B:
                    {
                        request = new AdapteeB().SpecificRequestB;
                    }
                    break;
            }
        }

        void ITarget.Request()
        {
            request.Invoke();
        }
    }
}


