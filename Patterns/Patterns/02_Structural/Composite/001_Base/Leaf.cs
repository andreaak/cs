using System;
using System.Diagnostics;

namespace Patterns.Structural.Composite._001_Base
{
    class Leaf : Component
    {
        public Leaf(string name)
            : base(name)
        {
        }

        public override void Operation()
        {
            Debug.WriteLine(name);
        }

        public override void Add(Component component)
        {
            throw new InvalidOperationException();
        }

        public override void Remove(Component component)
        {
            throw new InvalidOperationException();
        }

        public override Component GetChild(int index)
        {
            throw new InvalidOperationException();
        }
    }
}
