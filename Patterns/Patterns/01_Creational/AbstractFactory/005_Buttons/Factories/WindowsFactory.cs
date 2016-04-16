using Patterns._01_Creational.AbstractFactory._005_Buttons.Buttons;
using Patterns._01_Creational.AbstractFactory._005_Buttons.Windows;

namespace Patterns._01_Creational.AbstractFactory._005_Buttons.Factories
{
    // ConcreteFactory1
    class WindowsFactory : WidgetFactory
    {
        public override AbstractWindow CreateWindow()
        {
            return new WindowsForm();
        }

        public override AbstractButton CreateButton()
        {
            return new WindowsButton();
        }
    }
}
