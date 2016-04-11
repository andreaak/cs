using System;
using System.Diagnostics;

// Сменный адаптер.

namespace Patterns.Structural.Adapter._005_PluggableAdapter
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
            Debug.WriteLine("Target.Request");
        }
    }

    // ---------- Классы требующие адаптации ----------

    class AdapteeA
    {
        public void SpecificRequestA()
        {
            Debug.WriteLine("AdapteeA.SpecificRequestA");
        }
    }

    class AdapteeB
    {
        public void SpecificRequestB()
        {
            Debug.WriteLine("AdapteeB.SpecificRequestB");
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


