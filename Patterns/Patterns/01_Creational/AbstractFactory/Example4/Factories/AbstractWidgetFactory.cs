using System;

namespace Patterns.Creational.AbstractFactory.Example4
{
    // AbstractFactory
    abstract class WidgetFactory
    {
        public abstract AbstractWindow CreateWindow();
        public abstract AbstractButton CreateButton();
    }
}
