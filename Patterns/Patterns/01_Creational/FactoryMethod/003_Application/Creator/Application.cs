
namespace Creational.FactoryMethod._003_Application.Pattern
{
    // Creator
    abstract class Application
    {
        public abstract Document CreateDocument();
        public abstract void OpenDocument();
    }
}
