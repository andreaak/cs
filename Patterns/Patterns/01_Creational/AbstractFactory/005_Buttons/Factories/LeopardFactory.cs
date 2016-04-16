using Patterns._01_Creational.AbstractFactory._005_Buttons.Buttons;
using Patterns._01_Creational.AbstractFactory._005_Buttons.Windows;

namespace Patterns._01_Creational.AbstractFactory._005_Buttons.Factories
{
    // ConcreteFactory2
    class LeopardFactory : WidgetFactory
    {
        public override AbstractWindow CreateWindow()
        {
            return new LeopardForm();
        }

        public override AbstractButton CreateButton()
        {
            return new LeopardButton();
        }
    }
}
