
using Patterns._01_Creational.FactoryMethod._003_Application.Product;

namespace Patterns._01_Creational.FactoryMethod._003_Application.Creator
{
    // Creator
    abstract class Application
    {
        public abstract Document CreateDocument();
        public abstract void OpenDocument();
    }
}
