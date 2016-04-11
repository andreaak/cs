using System;

namespace Patterns.Structural.Composite._003_RecursionComposition
{
    abstract class Component
    {
        protected string name;

        public Component(string name)
        {
            this.name = name;
        }

        public abstract void Operation();
        public abstract void Build(int[] rules);
        public abstract void Add(int key, Component component);
    }
}
