using System;
using System.Diagnostics;

namespace Patterns._02_Structural.Composite._003_RecursionComposition
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

        public override void Build(int[] rules)
        {
            throw new InvalidOperationException();
        }

        public override void Add(int key, Component component)
        {
            throw new InvalidOperationException();
        }
    }
}
