using System.Windows.Forms;
using Patterns._01_Creational.AbstractFactory._005_Buttons.Buttons;
using Patterns._01_Creational.AbstractFactory._005_Buttons.Factories;
using Patterns._01_Creational.AbstractFactory._005_Buttons.Windows;

namespace Patterns._01_Creational.AbstractFactory._005_Buttons
{
    // Client
    class Client
    {
        AbstractWindow window;
        AbstractButton button;

        public Client(WidgetFactory factory)
        {
            window = factory.CreateWindow();
            button = factory.CreateButton();
        }

        public Form GetForm()
        {
            window.AddToCollection(button);
            return window;
        }
    }
}
