using Patterns._01_Creational.AbstractFactory._005_Buttons.Buttons;
using Patterns._01_Creational.AbstractFactory._005_Buttons.Windows;

namespace Patterns._01_Creational.AbstractFactory._005_Buttons.Factories
{
    class LinuxFactory : WidgetFactory
    {
        public override AbstractWindow CreateWindow()
        {
            return new LinuxForm();
        }

        public override AbstractButton CreateButton()
        {
            return new LinuxButton();
        }
    }
}
