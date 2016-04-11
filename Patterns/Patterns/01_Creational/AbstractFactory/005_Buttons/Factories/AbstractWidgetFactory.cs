using System;

namespace Patterns.Creational.AbstractFactory._005_Buttons
{
    // AbstractFactory
    abstract class WidgetFactory
    {
        public abstract AbstractWindow CreateWindow();
        public abstract AbstractButton CreateButton();
    }
}
