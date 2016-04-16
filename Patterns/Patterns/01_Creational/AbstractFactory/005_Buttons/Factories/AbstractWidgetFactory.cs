using Patterns._01_Creational.AbstractFactory._005_Buttons.Buttons;
using Patterns._01_Creational.AbstractFactory._005_Buttons.Windows;

namespace Patterns._01_Creational.AbstractFactory._005_Buttons.Factories
{
    // AbstractFactory
    abstract class WidgetFactory
    {
        public abstract AbstractWindow CreateWindow();
        public abstract AbstractButton CreateButton();
    }
}
