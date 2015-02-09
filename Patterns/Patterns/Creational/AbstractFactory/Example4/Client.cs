using System.Windows.Forms;

namespace Patterns.Creational.AbstractFactory.Example4
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
