using System;

namespace Patterns.Structural.Composite.Example3
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
